namespace DistributedCalculator.CLI.Messages {
    public class ResultNotCachedEventMessage {
        public ResultNotCachedEventMessage(object command) {
            this.command = command;
        }

        public object command { get; }
    }
}