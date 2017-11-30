using Akka.Actor;

namespace DistributedCalculator.Messages {
    public interface ICalculationCommandMessage {
        IActorRef ResultReceiver { get; }
        string QuestionDescription { get; }
    }
}