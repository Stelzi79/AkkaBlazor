using System;
using System.IO;
//using Akka.Actor;
//using Akka.Configuration;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorClientNode
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            Console.WriteLine("Create SeedNode ...");
            //string seedNodeConfig = File.ReadAllText("akkanode.conf");
            string seedNodeConfig = @"akka {
    actor.provider = cluster
    remote {
        dot-netty.tcp {
            port = 0 #let os pick random port
            hostname = localhost
        }
    }
    cluster {
        seed-nodes = [\""akka.tcp://AkkaBlazorCluster@localhost:8085\""]
    }
    }";
            //Config config = ConfigurationFactory.ParseString(seedNodeConfig);

            //ActorSystem system = ActorSystem.Create("AkkaBlazorCluster", config);

            //services.AddSingleton(system);

        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
