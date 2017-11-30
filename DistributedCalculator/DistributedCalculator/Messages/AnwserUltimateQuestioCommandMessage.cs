using Akka.Actor;

namespace DistributedCalculator.Messages {

    public sealed class AnwserUltimateQuestioCommandMessage : ICalculationCommandMessage {
        public AnwserUltimateQuestioCommandMessage(IActorRef resultReceiver) {
            ResultReceiver = resultReceiver;
        }

        public IActorRef ResultReceiver { get; }

        public string QuestionDescription => "Die Frage nach Frage nach dem Leben, dem Universum und dem ganzen Rest";
    }
}