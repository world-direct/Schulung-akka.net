using Akka.Actor;

namespace DistributedCalculator.CLI.Messages {

    public sealed class AnwserUltimateQuestioCommandMessage : ICalculationCommandMessage {
        public AnwserUltimateQuestioCommandMessage(IActorRef resultReceiver) {
            ResultReceiver = resultReceiver;
        }

        public IActorRef ResultReceiver { get; }
    }
}