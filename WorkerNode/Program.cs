using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Petabridge.Cmd.Host;
using SharedCalculation.BusinessDomain.Calculation.Actors;

namespace WorkerNode {
    class Program {


        private static ActorSystem system;
         
        static void Main(string[] args) {
            handler = new ConsoleEventDelegate(ConsoleEventCallback);
            SetConsoleCtrlHandler(handler, true);


            Console.WriteLine("WORKER");
            system = ActorSystem.Create("calculation");
            var cmd = PetabridgeCmd.Get(system);
            //cmd.Start();


            //system.ActorOf(Props.Create<CalculationCoordinatorActor>(), "calculationCoordinator");


            system.WhenTerminated.Wait();
        }
        
        static bool ConsoleEventCallback(int eventType) {
            if (eventType == 2) {
                CoordinatedShutdown.Get(system).Run().Wait();
                Console.WriteLine("Console window closing, death imminent");
            }
            return false;
        }

        static ConsoleEventDelegate handler; // Keeps it from getting garbage collected
        // Pinvoke
        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);

    }
}


