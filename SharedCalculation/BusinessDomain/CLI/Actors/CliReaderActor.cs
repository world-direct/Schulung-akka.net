using System;
using Akka.Actor;
using SharedCalculation.BusinessDomain.CLI.Messages;

namespace SharedCalculation.BusinessDomain.CLI {
    
    public class CliReaderActor : ReceiveActor {
        
        private static readonly string CLI_COMMAND_PARSER_NAME = "commandParser";

        public CliReaderActor()
        {
            Context.ActorOf(Props.Create<CliCommandParserActor>(), CLI_COMMAND_PARSER_NAME);

            Receive<AskUserForInputCommandMessage>(x => HandleAksUserForInput(x));
            Receive<InputParsedEventMessage>(x => Context.Parent.Forward(x));
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(x => {
                Context.Parent.Tell(new InputParsedEventMessage(InputParsedEventMessage.CommandType.InvalidCommand,
                    Double.MaxValue, Double.MaxValue));
                return Directive.Restart;
            });
        }

        private void HandleAksUserForInput(AskUserForInputCommandMessage message) {
            Console.WriteLine(message.Message);
            var input = Console.ReadLine();

            Context.Child(CLI_COMMAND_PARSER_NAME).Tell(new ParseCliInputCommandMessage(input));
        }
    }
}