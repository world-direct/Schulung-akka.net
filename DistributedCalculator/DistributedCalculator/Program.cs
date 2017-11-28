using System;
using Akka.Actor;
using DistributedCalculator.Actors;
using DistributedCalculator.Messages;

namespace DistributedCalculator {
    internal class Program {
        private static void Main(string[] args) {
            using (var system = ActorSystem.Create("CalculationSystem")) {
                var calculator = system.ActorOf(Props.Create<CalculatorActor>(), "calculator");
                var random = new Random(100);
                while (Console.ReadLine() != "c") {
                    calculator.Tell(new AddMessage(random.Next(), random.Next()));
                }
                system.Terminate();
                system.WhenTerminated.Wait();
            }
        }
    }
}