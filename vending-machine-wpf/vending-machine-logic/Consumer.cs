using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using vending_machine_wpf.Managers;

namespace vending_machine_wpf;

internal class Consumer {
    // Dequeues bottle from their designated Bottle buffer,
    // as long as local buffer is not empty
    internal static Bottle Consume(Queue<Bottle> box) {
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
                
                return currentBottle;

            }
            finally {
                Monitor.Exit(box);
            }
        }
    }
}
