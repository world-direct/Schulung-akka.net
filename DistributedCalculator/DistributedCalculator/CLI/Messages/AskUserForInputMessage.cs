using Akka.Actor;

namespace DistributedCalculator.CLI.Messages
{
        
        public sealed class AskUserForInputCommandMessage {
            public string Message { get; }

            public AskUserForInputCommandMessage(string message) {
                Message = message;
            }
    }
}