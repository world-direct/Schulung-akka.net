using Akka.Actor;
using DistributedCalculator.Actors;
using DistributedCalculator.Messages;

namespace DistributedCalculator {
    internal class Program {
        private static void Main(string[] args) {
            using (var system = ActorSystem.Create("CalculationSystem")) {
                var calculator = system.ActorOf(Props.Create<CalculatorActor>(), "calculator");
                calculator.Tell(new AddMessage(4, 6));

                system.WhenTerminated.Wait();
            }
        }
    }
}