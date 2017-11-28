using System;
using Akka.Actor;
using DistributedCalculator.CLI.Messages;

namespace SharedCalculation.BusinessDomain.Calculation.Actors
{
    public class SummationWorkerActor : ReceiveActor {

    
        public SummationWorkerActor() {
            Receive<AddCommandCommandMessage>(x => HandleAddMessage(x));
        }

   
        private void HandleAddMessage(AddCommandCommandMessage addCommandCommandMessage) {

            Console.WriteLine($"Ich berechne die Aufgabe {addCommandCommandMessage.Summand1}+{addCommandCommandMessage.Summand2}");
            var result = addCommandCommandMessage.Summand1 + addCommandCommandMessage.Summand2;
            addCommandCommandMessage.ResultReceiver.Tell(new ResultCalculatedEventMessage(result, addCommandCommandMessage));
        }
    }
}