using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AL.WebshopService.DAL;
using AL.WebshopService.Events.EventPublishers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Minor.Miffy;
using Minor.Miffy.MicroServices.Events;
using Minor.Miffy.MicroServices.Host;
using Minor.Miffy.RabbitMQBus;
using RabbitMQ.Client;

namespace AL.WebshopService
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

            string dburl = Environment.GetEnvironmentVariable("dburl");
            string mqurl = Environment.GetEnvironmentVariable("mqurl");

            IBusContext<IConnection> context = null;
            var created = false;
            while (!created)
            {
                try
                {
                    context = new RabbitMqContextBuilder()
                        .WithExchange("Webshop")
                        .WithConnectionString(mqurl)
                        .CreateContext();
                    created = true;
                }
                catch (BusConfigurationException e)
                {
                    Thread.Sleep(1000);
                }
            }

            var builder = new MicroserviceHostBuilder()
                .WithBusContext(context)
                .RegisterDependencies(services =>
                {
                    services.AddDbContext<WebshopContext>(options => options.UseSqlServer(dburl));

                })
                .WithQueueName("WebshopSerivce.Bestellingen")
                .UseConventions();

            var host = builder.CreateHost();
            host.Start();



            services.AddControllers();
            services.AddDbContext<WebshopContext>(options => options.UseSqlServer(dburl));
            services.AddSingleton(context);
            services.AddSingleton<BestellingGeplaatstEventPublisher, BestellingGeplaatstEventPublisher>();
            services.AddTransient<IEventPublisher, EventPublisher>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                bool created = false;

                while (!created)
                {
                    try
                    {

                        serviceScope.ServiceProvider.GetService<WebshopContext>().Database.EnsureCreated();

                        created = true;
                    }
                    catch (Exception e)
                    {
                        Thread.Sleep(1000);
                    }
                }
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
