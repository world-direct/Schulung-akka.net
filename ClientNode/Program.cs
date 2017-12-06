using System;
using System.Runtime.InteropServices;
using Akka.Actor;
using Petabridge.Cmd.Cluster;
using Petabridge.Cmd.Host;
using SharedCalculation.BusinessDomain.CLI.Actors;

namespace ClientNode
{
    class Program
    {
        private static ActorSystem system;

        static void Main(string[] args) {
            Console.WriteLine("CLIENT");

            system = ActorSystem.Create("calculation");

            var clientActor = system.ActorOf(Props.Create<CliClientActor>(), "cliClient");
            var cmd = PetabridgeCmd.Get(system);
            cmd.RegisterCommandPalette(ClusterCommands.Instance);
            cmd.Start();

            clientActor.Tell(new AskUserForInputCommandMessage("Frage.."));

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


