using Akka.Actor;

namespace DistributedCalculator.CLI.Messages {
    public interface ICalculationCommandMessage {
        IActorRef ResultReceiver { get; }
    }
}