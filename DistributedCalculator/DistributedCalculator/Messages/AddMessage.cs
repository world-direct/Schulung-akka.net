namespace DistributedCalculator.Messages {
    
    public sealed class AddMessage {
        
        public AddMessage(int summand1, int summand2) {
            Summand1 = summand1;
            Summand2 = summand2;
        }

        public int Summand1 { get; }

        public int Summand2 { get; }
    }
}