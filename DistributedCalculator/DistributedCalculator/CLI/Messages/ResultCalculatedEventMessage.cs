namespace DistributedCalculator.CLI.Messages {
    public class ResultCalculatedEventMessage {
        public double Result { get; }
        public object command { get; }

        public ResultCalculatedEventMessage(double result, object command) {
            Result = result;
            this.command = command;
        }
    }
}