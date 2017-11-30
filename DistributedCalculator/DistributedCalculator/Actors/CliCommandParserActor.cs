using System;
using Akka.Actor;
using DistributedCalculator.Messages;

namespace DistributedCalculator.Actors {
    public class CliCommandParserActor : ReceiveActor {
        public CliCommandParserActor() {
            Receive<ParseCliInputCommandMessage>(x => HandleParseCliInput(x));
        }
        
        private void HandleParseCliInput(ParseCliInputCommandMessage inputMessage) {

            if (inputMessage.CliInput.Equals("x")) {
                Context.System.Terminate();
                return;
            }

            if (inputMessage.CliInput.Equals("question", StringComparison.CurrentCultureIgnoreCase)) {
                Sender.Tell(new InputParsedEventMessage(InputParsedEventMessage.CommandType.UltimateQuestion, Double.NegativeInfinity, Double.NegativeInfinity));
                return;
            }

            var splitInput = inputMessage.CliInput.Split('+');

            if (splitInput.Length != 2) {
                Sender.Tell(new InputParsedEventMessage(InputParsedEventMessage.CommandType.InvalidCommand, Double.NegativeInfinity, Double.NegativeInfinity));
                return;
            }

            var commandParameter = inputMessage.CliInput[splitInput[0].Length];

            var operand1 = Convert.ToDouble(splitInput[0]);
            var operand2 = Convert.ToDouble(splitInput[1]);

            InputParsedEventMessage.CommandType commandType;

            switch (commandParameter) {
                case '+':
                    commandType = InputParsedEventMessage.CommandType.Add;
                    break;
                default:
                    commandType = InputParsedEventMessage.CommandType.InvalidCommand;
                    break;
            }
            
            Sender.Tell(new InputParsedEventMessage(commandType, operand1, operand2));

        }
    }
}