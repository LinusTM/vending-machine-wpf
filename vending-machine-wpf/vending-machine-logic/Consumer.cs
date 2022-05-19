using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using vending_machine_wpf.EventArgs;
using vending_machine_wpf.Managers;

namespace vending_machine_wpf;

internal class Consumer {
    internal event EventHandler<BottleEventArgs> consumeEvent;
    public void BottleConsumed(Bottle bottle)
    {
        consumeEvent?.Invoke(this, new BottleEventArgs(bottle));
    }
    
    // Dequeues bottle from their designated Bottle buffer,
    // as long as local buffer is not empty
    internal void Consume(Queue<Bottle> box) {
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
                
                BottleConsumed(currentBottle);
                Debug.WriteLine($"CONSUMER: just drank {currentBottle.Type}{currentBottle.Number}");
            }
            finally {
                Monitor.Exit(box);
            }
        }
    }
}
