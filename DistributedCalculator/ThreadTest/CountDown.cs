using System.Threading;

namespace ThreadTest {

    // Invariant of the class: A countdown stops with 0 and cannot become negatice.
    public class CountDown {

        // Internal state of the class. Value must not be negative.
        private int value;

        public CountDown(int start) {
            value = start;
        }

        public void Decrement() {
            // We ensure that the countdown never becomes negative
            if (value > 0) {
                Thread.Sleep(100); // Some longer running (blocking) operation
                value -= 1;
            }
        }

        public int Value => value;
    }
}