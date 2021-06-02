using System;
using System.Threading.Tasks;
using Hania.NetCore.MasterBus.Core.Events;

namespace HaniaNetCore.MasterBus.Sample.Events
{
    public class BlogCreated : IEvent
    {
        public BlogCreated(Guid blogId, string title, string content)
        {
            BlogId = blogId;
            CorrelationId = blogId;
            Title = title;
            Content = content;
        }

        public Guid CorrelationId { get; set; }
        public Guid BlogId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
    
    
    public class BlogCreatedHandler : IEventHandler<BlogCreated>
    {
        public async Task Handle(BlogCreated Event)
        {
            await Console.Out.WriteLineAsync($"IEventHandler => BlogCreatedHandler => {Event.CorrelationId}");
            
        }
    }
}