using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;
using Hania.NetCore.MasterBus;
using Hania.NetCore.MasterBus.EventSourcing;
using Hania.NetCore.MasterBus.EventSourcing.Enums;
using Hania.NetCore.MasterBus.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;

namespace HaniaNetCore.MasterBus.Sample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Hania.NetCore.MasterBus.Sample", Version = "v1"});
            });

            services.AddMicroservice("SampleService",microservice =>
            {
                microservice.UseMessaging(config =>
                {
                    config.UseRabbitMQ(c =>
                    {
                        c.Host("amqp://guest:guest@localhost:5672");
                        c.Exchange("sample_exchange",ExchangeType.Topic);
                        c.Queue("sample_queue");
                    });
                });
                
                microservice.UseEventSourcing(config =>
                {
                    config.UseMongo(c =>
                    {
                        c.Host("mongodb://localhost");
                        c.Database("Sample");
                        c.Collection("EventSources");
                    });
                });
                
                microservice.UseOutbox(config =>
                {

                    config.Processor(TimeSpan.FromSeconds(2));
                    
                    config.UseMongo(c =>
                    {
                        c.Host("mongodb://localhost");
                        c.Database("Sample");
                        c.Collection("Outbox","Inbox");
                    });
                });
                
            });
          
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hania.NetCore.MasterBus.Sample v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}