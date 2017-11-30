namespace DistributedCalculator.Messages {
    public class ResultCalculatedEventMessage {
        public double Result { get; }
        public ICalculationCommandMessage command { get; }

        public ResultCalculatedEventMessage(double result, ICalculationCommandMessage command) {
            Result = result;
            this.command = command;
        }
    }
}