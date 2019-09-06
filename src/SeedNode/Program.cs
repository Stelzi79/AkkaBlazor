using System;
using System.IO;
using Akka.Actor;
using Akka.Configuration;

namespace SeedNode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create SeedNode ...");
            string seedNodeConfig = File.ReadAllText("akkanode.conf");
            Config config = ConfigurationFactory.ParseString(seedNodeConfig);

            ActorSystem system = ActorSystem.Create("AkkaBlazorCluster", config);

            IActorRef localecho = system.ActorOf<EchoActor>("SeedNodeEchoActor");
            //localecho.Tell("Actor system started and EchoActor added!");
            Console.ReadLine();
        }
    }
}
