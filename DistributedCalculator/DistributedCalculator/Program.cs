using System;
using Akka.Actor;
using DistributedCalculator.CLI.Messages;
using SharedCalculation.BusinessDomain.CLI;

namespace DistributedCalculator {

    internal class Program {

        private static void Main(string[] args) {
            using (var system = ActorSystem.Create("CalculationSystem")) {
                var cliClientActor = system.ActorOf(Props.Create<CliClientActor>(), "cliClient");

                cliClientActor.Tell(new AskUserForInputCommandMessage("Frage.."));
                system.WhenTerminated.Wait();
            }
        }
    }
}