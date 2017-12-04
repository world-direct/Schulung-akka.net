using System;
using Akka.Actor;
using Akka.Routing;
using SharedCalculation.BusinessDomain.Calculation.Actors;
using SharedCalculation.BusinessDomain.Calculation.Messages;
using SharedCalculation.BusinessDomain.CLI.Messages;

namespace SharedCalculation.BusinessDomain.CLI.Actors {
    public partial class CliClientActor : ReceiveActor {
        private static readonly string CLI_COMMAND_PARSER_NAME = "commandParser";
        private static readonly string CalculationCoordinatorName = "calculationCoordinators";
        private static readonly string CONSOLE_READER_NAME = "consoleReader";
        
            public CliClientActor()
            {
                Context.ActorOf(Props.Create<CalculationCoordinatorActor>(), CalculationCoordinatorName);
                Context.ActorOf(Props.Create<CliReaderActor>(), CONSOLE_READER_NAME);

                Receive<AskUserForInputCommandMessage>(x => HandleAskUserForInput(x));
                Receive<InputParsedEventMessage>(x => HandleInputParsed(x));
                Receive<ResultCalculatedEventMessage>(x => HandleCalculationResult(x));
            }


            private void HandleCalculationResult(ResultCalculatedEventMessage x)
            {
                Console.WriteLine($"Ergebnis von \"{x.command.QuestionDescription}\" ist {x.Result}");

            }

            private void HandleInputParsed(InputParsedEventMessage parseCliInputCommandMessage)
            {

                var calculator = Context.Child(CalculationCoordinatorName);
                switch (parseCliInputCommandMessage.Command)
                {

                    case InputParsedEventMessage.CommandType.Add:
                        calculator.Tell(new AddCommandMessage(parseCliInputCommandMessage.Operand1, parseCliInputCommandMessage.Operand2, Self));
                        break;
                    case InputParsedEventMessage.CommandType.UltimateQuestion:
                        calculator.Tell(new UltimateQuestionCommandMessage(Self));
                        break;
                    case InputParsedEventMessage.CommandType.InvalidCommand:
                        Self.Tell(new AskUserForInputCommandMessage("CliInput fehlerhaft. Probieren Sie es nochmals: "));
                        return;

                }
                Self.Tell(new AskUserForInputCommandMessage("Eine weitere Frage: "));

            }
        
            private void HandleAskUserForInput(AskUserForInputCommandMessage askUserForInputCommandMessage)
            {
                Context.Child(CONSOLE_READER_NAME).Tell(askUserForInputCommandMessage);
            }
        
        }

    }