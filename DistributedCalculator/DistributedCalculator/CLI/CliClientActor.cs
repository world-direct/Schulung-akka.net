using System;
using Akka.Actor;
using Akka.Routing;
using DistributedCalculator.CLI.Messages;
using SharedCalculation.BusinessDomain.Calculation.Actors;
using SharedCalculation.BusinessDomain.CLI.Messages;

namespace SharedCalculation.BusinessDomain.CLI
{
    public class CliClientActor : ReceiveActor {
        private static readonly string CalculationCoordinatorName = "calculationCoordinator";
        private static readonly string CONSOLE_READER_NAME = "consoleReader";
        
        public CliClientActor() {
            Context.ActorOf(Props.Create<CalculationCoordinatorActor>(), CalculationCoordinatorName);
            Context.ActorOf(Props.Create<CliReaderActor>(), CONSOLE_READER_NAME);
            
            Receive<AskUserForInputCommandMessage>(x => HandleAskUserForInput(x));
            Receive<InputParsedEventMessage>(x => HandleInputParsed(x));
            Receive<ResultCalculatedEventMessage>(x => HandleCalculationResult(x));
            Receive<ReceiveTimeout>(x => HandleReceiveTimeout(x));
        }
     

        private void HandleCalculationResult(ResultCalculatedEventMessage x) {
            Console.WriteLine($"Result is {x.Result}");

            Self.Tell(new AskUserForInputCommandMessage("Eine neue Frage"));

        }

        private void HandleInputParsed(InputParsedEventMessage inputParsedEventMessage) {

            var calculator = Context.Child(CalculationCoordinatorName);
            switch (inputParsedEventMessage.Command) {

                case InputParsedEventMessage.CommandType.Add:
                    calculator.Tell(new AddCommandCommandMessage(inputParsedEventMessage.Operand1, inputParsedEventMessage.Operand2, Self));
                    break;
                case InputParsedEventMessage.CommandType.UltimateQuestion:
                    calculator.Tell(new AnwserUltimateQuestioCommandMessage(Self));
                    break;
                case InputParsedEventMessage.CommandType.InvalidCommand:
                    Self.Tell(new AskUserForInputCommandMessage("Input fehlerhaft. Probieren Sie es nochmals: "));
                    return;
            }
        }


        private void HandleAskUserForInput(AskUserForInputCommandMessage askUserForInputCommandMessage) {
            Context.Child(CONSOLE_READER_NAME).Tell(askUserForInputCommandMessage);
        }

        private void HandleReceiveTimeout(ReceiveTimeout m) {
            Self.Tell(new AskUserForInputCommandMessage("Die Rechnung konnte nicht berrechnet werden. Probieren Sie es nochmals: "));
        }
    }
}