using Akka.Actor;

namespace SharedCalculation.BusinessDomain.Calculation.Messages {

    public sealed class UltimateQuestionCommandMessage : ICalculationCommandMessage {
        public UltimateQuestionCommandMessage(IActorRef resultReceiver) {
            ResultReceiver = resultReceiver;
        }

        public IActorRef ResultReceiver { get; }

        public string QuestionDescription => "Die Frage nach Frage nach dem Leben, dem Universum und dem ganzen Rest";

    }
}