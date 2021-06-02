using System;
using System.Threading.Tasks;
using Hania.NetCore.MasterBus.Core.Commands;
using Hania.NetCore.MasterBus.Core.Events;
using HaniaNetCore.MasterBus.Sample.Events;

namespace HaniaNetCore.MasterBus.Sample.Commands
{
    public class CreateBlog : ICommand
    {
        
        public CreateBlog(Guid blogId, string title, string content)
        {
            BlogId = blogId;
            CorrelationId = blogId;
            Title = title;
            Content = content;
        }

        public Guid CorrelationId { get; set; }
        public Guid BlogId { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
    }
    
    
    public class CreateBlogHandler : ICommandHandler<CreateBlog>
    {
        private readonly IEventBus _eventBus;

        public CreateBlogHandler(IEventBus eventBus)
        {
            _eventBus = eventBus;
        }

        public async Task Handle(CreateBlog request)
        {
            await Console.Out.WriteLineAsync($"ICommandHandler => CreateBlogHandler => {request.CorrelationId.ToString()}");
            await _eventBus.Publish(new BlogCreated(request.BlogId, request.Title, request.Content));
        }
    }
}