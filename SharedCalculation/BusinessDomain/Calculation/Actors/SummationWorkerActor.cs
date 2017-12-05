using System;
using Akka.Actor;
using Akka.Cluster.Tools.PublishSubscribe;
using SharedCalculation.BusinessDomain.Calculation.Messages;

namespace SharedCalculation.BusinessDomain.Calculation.Actors
{
    public class SummationWorkerActor : ReceiveActor {

        private IActorRef cacheResultPublisher;
    
        public SummationWorkerActor() {

            Receive<AddCommandMessage>(x => HandleAddMessage(x));
        }

        protected override void PreStart() {
            base.PreStart();
            cacheResultPublisher = DistributedPubSub.Get(Context.System).Mediator;
        }

        private void HandleAddMessage(AddCommandMessage addCommandMessage) {

            Console.WriteLine($"Ich berechne die Aufgabe {addCommandMessage.Summand1}+{addCommandMessage.Summand2}");
            var result = addCommandMessage.Summand1 + addCommandMessage.Summand2;
            var resultMessage = new ResultCalculatedEventMessage(result, addCommandMessage);
            addCommandMessage.ResultReceiver.Tell(resultMessage);
            cacheResultPublisher.Tell(new Publish("resultCache", resultMessage));
        }
    }
}