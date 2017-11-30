namespace DistributedCalculator.Messages {
    public sealed class ParseCliInputCommandMessage {

        public string CliInput { get; }

        public ParseCliInputCommandMessage(string cliInput) {
            CliInput = cliInput;
        }
    }
}