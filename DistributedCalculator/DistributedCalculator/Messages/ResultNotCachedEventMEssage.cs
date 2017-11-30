namespace DistributedCalculator.Messages {
    public class ResultNotCachedEventMessage {
        public ResultNotCachedEventMessage(object command) {
            this.command = command;
        }

        public object command { get; }
    }
}