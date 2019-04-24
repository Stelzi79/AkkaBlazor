using Akka.Actor;
using Akka.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;
using System.Linq;

namespace BlazorServerNode.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("Create SeedNode ...");
            string seedNodeConfig = File.ReadAllText("akkanode.conf");
            //        string seedNodeConfig = @"akka {
            //actor.provider = cluster
            //remote {
            //    dot-netty.tcp {
            //        port = 0 #let os pick random port
            //        hostname = localhost
            //    }
            //}
            //cluster {
            //    seed-nodes = [\""akka.tcp://AkkaBlazorCluster@localhost:8085\""]
            //}
            //}";
            Config config = ConfigurationFactory.ParseString(seedNodeConfig);
            ActorSystem system = ActorSystem.Create("AkkaBlazorCluster", config);
            IActorRef localecho = system.ActorOf<EchoActor>("BalzorServerActor");

            Akka.Cluster.Cluster Cluster = Akka.Cluster.Cluster.Get(system);


            ActorSelection selection = system.ActorSelection("*");

            //selection.Tell("test");


            IActorRef actor1 = selection.ResolveOne(new TimeSpan(0, 1, 0)).GetAwaiter().GetResult();

            actor1.Tell("Test from Blazor");
            //IActorRef actor2 = selection.ResolveOne(new TimeSpan(0, 1, 0)).GetAwaiter().GetResult();


            services.AddSingleton(system);
            services.AddMvc().AddNewtonsoftJson();
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.UseBlazor<Client.Startup>();
        }
    }
}
