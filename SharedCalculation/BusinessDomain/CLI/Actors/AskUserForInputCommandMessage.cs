namespace SharedCalculation.BusinessDomain.CLI.Actors {
    
    public sealed class AskUserForInputCommandMessage {
        public string Message { get; }

        public AskUserForInputCommandMessage(string message) {
            Message = message;
        }
    }
}