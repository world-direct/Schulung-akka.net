using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadTest {

    class Program {
        
        private static readonly CountDown CountDown = new CountDown(10);

        public static void Main() {
            Task.Run(() => Decrementer());
            Task.Run(() => Decrementer());

            Console.WriteLine("Counting down ...");
            Console.ReadLine();

            // Verify that the countdown has not become negative
            Console.WriteLine(CountDown.Value >= 0
                ? $"Everything OK, countdown is NOT negative (value: {CountDown.Value})"
                : $"Ooops, something wrent seriously wrong: The countdown is NEGATIVE! (value: {CountDown.Value})");

            Console.ReadLine();
        }

        private static void Decrementer() {
            for (int i = 0; i < 1000; i++) {
                CountDown.Decrement();
            }
        }
    }
}
