﻿using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace BlazorServerNode.Server
{
    public class EchoActor : UntypedActor
    {
        private const string received = "received";

        protected override void OnReceive(object message)
        {
            switch (message)
            {
                case string strg when strg != received:
                    Console.WriteLine($"Received a EchoString '{strg}'");
                    //Sender.Tell(received);
                    break;
                case String strg:
                    Console.WriteLine($"EchoActor has responded!");
                    break;
                default:
                    break;
            }

        }
    }
}
