using System.Diagnostics;

namespace Flaske_Automat;

internal class Consumer {
    internal static void Consume(Queue<Bottle> box) {
        while(true) {
            Monitor.Enter(box);

            try {
                // Check/wait if no available consumable
                while(box.Count == 0) {
                    Thread.Sleep(100/15);
                    Monitor.Wait(box);
                }

                // Toss nearest soda and notify
                Bottle currentBottle = box.Dequeue();
                Debug.WriteLine($"CONSUMER: just drank {currentBottle.Type}{currentBottle.Number}");

            }
            finally {
                Monitor.Exit(box);
            }
        }
    }
}
