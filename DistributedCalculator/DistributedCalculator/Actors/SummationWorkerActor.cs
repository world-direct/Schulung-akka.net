using Akka.Actor;
using DistributedCalculator.Messages;

namespace DistributedCalculator.Actors
{
    public class SummationWorkerActor : ReceiveActor {

    
        public SummationWorkerActor() {
            Receive<AddCommandMessage>(x => HandleAddMessage(x));
        }

   
        private void HandleAddMessage(AddCommandMessage addCommandMessage) {
            //Console.WriteLine($"Ich berechne die Aufgabe {addCommandMessage.Summand1}+{addCommandMessage.Summand2}");
            var result = addCommandMessage.Summand1 + addCommandMessage.Summand2;
            addCommandMessage.ResultReceiver.Tell(new ResultCalculatedEventMessage(result, addCommandMessage));
        }
    }
}