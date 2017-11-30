using System;
using Akka.Actor;
using Akka.Routing;
using DistributedCalculator.CLI.Messages;

namespace DistributedCalculator.Calculation.Actors {
    public class CalculationCoordinatorActor : ReceiveActor {

        private static readonly string SumCalculationName = "summationWorker";
        private static readonly string UltimateQuestionWorker = "ultimateQuestionWorker";
        

        public CalculationCoordinatorActor() {
            Context.ActorOf(Props.Create<SummationWorkerActor>().WithRouter(FromConfig.Instance), SumCalculationName);
            Context.ActorOf(Props.Create<UltimateQuestionLifeWorker>().WithRouter(FromConfig.Instance), UltimateQuestionWorker);
            
            Receive<AddCommandMessage>(x => {
                Context.Child(SumCalculationName).Forward(x);
            });
            Receive<AnwserUltimateQuestioCommandMessage>(x => Context.Child(UltimateQuestionWorker).Forward(x));
        }

        protected override void PostStop() {
            base.PostStop();
            Console.WriteLine("CalculationCoordinatorActor stopped");

        }
    }
}