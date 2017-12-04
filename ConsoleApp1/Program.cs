using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using SharedCalculation.BusinessDomain.CLI;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var system = ActorSystem.Create("Calculation")) {
                system.ActorOf(Props.Create<CliClientActor>(), "cliClient" );
                system.WhenTerminated.Wait();

            }
        }
    }
}
