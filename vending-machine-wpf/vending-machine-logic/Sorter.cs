using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using vending_machine_wpf.EventArgs;

namespace vending_machine_wpf;

public class Sorter {
    internal event EventHandler<BottleEventArgs> sortEvent;
    internal event EventHandler<BufferEventArgs> sodaBufferEvent;
    internal event EventHandler<BufferEventArgs> beerBufferEvent;
    
    public void BottleSorted(Bottle bottle)
    {
        sortEvent?.Invoke(this, new BottleEventArgs(bottle));
    }
    
    public void SodaBufferUpdate(Queue<Bottle> bottles)
    {
        sodaBufferEvent?.Invoke(this, new BufferEventArgs(bottles));
    }
    
    public void BeerBufferUpdate(Queue<Bottle> bottles)
    {
        beerBufferEvent?.Invoke(this, new BufferEventArgs(bottles));
    }
    
    // Sorts bottles depending on their type,
    // then returns the dequeued bottle
    public void Sort(Queue<Bottle> boxOfBottles, Queue<Bottle> beerBox, Queue<Bottle> sodaBox) {
        while(true) {
            Monitor.Enter(boxOfBottles);

            try {
                // Check/wait if no Bottles are in boxOfBottles
                while(boxOfBottles.Count == 0) {
                    Thread.Sleep(100/15);
                    Monitor.Wait(boxOfBottles);
                }
                
                // Dequeue next bottle in line, then send an event change
                Bottle currentBottle = boxOfBottles.Dequeue();
                BottleSorted(currentBottle);
                
                switch(currentBottle.Type) {
                    case "Beer":
                        Monitor.Enter(beerBox);

                        try {
                            // Put in beerBox if Bottle type == "Beer"
                            beerBox.Enqueue(currentBottle);
                            BeerBufferUpdate(beerBox);
                            
                            Debug.WriteLine($"SORTER: Moved beer{currentBottle.Number} to beerBox");

                            // Notify threads: Update to beerBox queue
                            Monitor.PulseAll(beerBox);
                        } 
                        finally {
                            Monitor.Exit(beerBox);
                        }
                        break;
                    case "Soda":
                        Monitor.Enter(sodaBox);
                        try {
                            // Put in sodaBox if Bottle type == "Soda"
                            sodaBox.Enqueue(currentBottle);
                            SodaBufferUpdate(sodaBox);
                            
                            Debug.WriteLine($"SORTER: Moved soda{currentBottle.Number} to sodaBox");

                            // Notify threads: Update to sodaBox queue
                            Monitor.PulseAll(sodaBox);
                        }
                        finally {
                            Monitor.Exit(sodaBox);
                        }
                        break; 
                }
            } 
            finally {
                Monitor.Exit(boxOfBottles);
            }
        }
    }
}
