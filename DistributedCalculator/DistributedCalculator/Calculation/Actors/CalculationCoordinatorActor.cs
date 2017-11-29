using System;
using Akka.Actor;
using Akka.Routing;
using DistributedCalculator.CLI.Messages;

namespace SharedCalculation.BusinessDomain.Calculation.Actors {
    public class CalculationCoordinatorActor : ReceiveActor {

        private static readonly string SumCalculationName = "summationWorker";
        private static readonly string UltimateQuestionWorker = "ultimateQuestionWorker";
        

        public CalculationCoordinatorActor() {
            Context.ActorOf(Props.Create<SummationWorkerActor>(), SumCalculationName);
            Context.ActorOf(Props.Create<UltimateQuestionLifeWorker>(), UltimateQuestionWorker);
            
            Receive<AddCommandMessage>(x => Context.Child(SumCalculationName).Forward(x));
            Receive<AnwserUltimateQuestioCommandMessage>(x => Context.Child(UltimateQuestionWorker).Forward(x));
        }

        protected override void PostStop() {
            base.PostStop();
            Console.WriteLine("CalculationCoordinatorActor stopped");

        }
    }
}