using Akka.Actor;

namespace SharedCalculation.BusinessDomain.Calculation.Messages {
    public interface ICalculationCommandMessage {
        IActorRef ResultReceiver { get; }
        string QuestionDescription { get; }
    }
}