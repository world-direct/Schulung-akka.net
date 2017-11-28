using System;
using Akka.Actor;
using DistributedCalculator.Messages;

namespace DistributedCalculator.Actors {
    
    public sealed class CalculatorActor : ReceiveActor {
        
        private int calculationCount = 0;

        public CalculatorActor() {
            Console.WriteLine($"New actor {Context.Self.Path} created.");
            Receive<AddMessage>(m => HandleAdd(m));
        }

        private void HandleAdd(AddMessage m) {
            calculationCount += 1;
            Console.WriteLine($"Calculating {m.Summand1} + {m.Summand2} = {m.Summand1 + m.Summand2}.{Environment.NewLine}Number of calculation: {calculationCount}");
        }
    }
}