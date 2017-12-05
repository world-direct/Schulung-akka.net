using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Cluster;
using Akka.Cluster.Tools.PublishSubscribe;
using Akka.Util.Internal;
using SharedCalculation.BusinessDomain.Calculation.Actors.CalculationResult.Messages;
using SharedCalculation.BusinessDomain.Calculation.Messages;

namespace SharedCalculation.BusinessDomain.Calculation.Actors.CalculationResult {
    public class CalculationResultStoreActor  : ReceiveActor{

        private Dictionary<string, double> cachedResults = new Dictionary<string, double>();

        public CalculationResultStoreActor() {
            Receive<AddCommandMessage>(x => HandleGetResultFromCacheAdd(x));
            Receive<UltimateQuestionCommandMessage>(x => HandleUltimateQuestion(x));
            Receive<ResultCalculatedEventMessage>(x => HandleCalculationResult(x));

            //DistributedPubSub.Get(Context.System).Mediator.Tell(new Subscribe("resultCache", Self));

        }

        private void HandleUltimateQuestion(UltimateQuestionCommandMessage ultimateQuestionCommandMessage) {
            var key = $"ultimateQuestion";
            HandleCalculationWithCacheKey(ultimateQuestionCommandMessage, key);


        }

        private void HandleCalculationResult(ResultCalculatedEventMessage resultCalculatedEventMessage) {
            if (resultCalculatedEventMessage.command is AddCommandMessage) {
                var addMessage = resultCalculatedEventMessage.command as AddCommandMessage;
                var key = $"add_{addMessage.Summand1}_{addMessage.Summand2}";
                cachedResults.AddOrSet(key, resultCalculatedEventMessage.Result);
            }
            if (resultCalculatedEventMessage.command is UltimateQuestionCommandMessage)
            {
                var key = $"ultimateQuestion";
                cachedResults.AddOrSet(key, resultCalculatedEventMessage.Result);
            }
        }

        private void HandleGetResultFromCacheAdd(AddCommandMessage addCommand) {
            var key = $"add_{addCommand.Summand1}_{addCommand.Summand2}";
            HandleCalculationWithCacheKey(addCommand, key);
        }

        private void HandleCalculationWithCacheKey(ICalculationCommandMessage add, string key) {
            double cachedResult;
            if (cachedResults.TryGetValue(key, out cachedResult)) {
                Console.WriteLine($"Aufgabe gefunden im Cache. Result ist {cachedResult}");
                add.ResultReceiver.Tell(new ResultCalculatedEventMessage(cachedResult, add), Sender);
                return;
            }

            Sender.Tell(new ResultNotCachedMessage(add));
        }
    }
}