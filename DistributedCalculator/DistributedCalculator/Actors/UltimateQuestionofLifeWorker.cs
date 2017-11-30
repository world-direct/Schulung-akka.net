using System;
using System.Threading;
using Akka.Actor;
using DistributedCalculator.Messages;

namespace DistributedCalculator.Actors
{
    public class UltimateQuestionLifeWorker : ReceiveActor {

        private IActorRef cacheResultPublisher;
    
        public UltimateQuestionLifeWorker() {

            Receive<AnwserUltimateQuestioCommandMessage>(x => HandleUltimateQuestion(x));
        }

    
        private void HandleUltimateQuestion(AnwserUltimateQuestioCommandMessage addMessage) {

            //Console.WriteLine($"Ich berechne die Frage nach dem Leben, dem Universum und dem ganzen Rest");
            // simuliere lange berechnung
              Thread.Sleep(TimeSpan.FromSeconds(3));
            addMessage.ResultReceiver.Tell(new ResultCalculatedEventMessage(42, addMessage));
        }
    }
}