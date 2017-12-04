namespace SharedCalculation.BusinessDomain.Calculation.Actors.CalculationResult.Messages {
    public class ResultNotCachedMessage {
        public ResultNotCachedMessage(object command) {
            this.command = command;
        }

        public object command { get; }


    }
}