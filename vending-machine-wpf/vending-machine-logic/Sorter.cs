using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

namespace vending_machine_wpf;

internal class Sorter {
    public static Bottle Sort(Queue<Bottle> boxOfBottles, Queue<Bottle> beerBox, Queue<Bottle> sodaBox) {
        while(true) {
            Monitor.Enter(boxOfBottles);

            try {
                // Check/wait if no Bottles are in boxOfBottles
                while(boxOfBottles.Count == 0) {
                    Thread.Sleep(100/15);
                    Monitor.Wait(boxOfBottles);
                }

                Bottle currentBottle = boxOfBottles.Dequeue();
                switch(currentBottle.Type) {
                    case "Beer":
                        Monitor.Enter(beerBox);

                        try {
                            // Put in beerBox if Bottle type == "Beer"
                            beerBox.Enqueue(currentBottle);
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
                            Debug.WriteLine($"SORTER: Moved soda{currentBottle.Number} to sodaBox");

                            // Notify threads: Update to sodaBox queue
                            Monitor.PulseAll(sodaBox);
                        }
                        finally {
                            Monitor.Exit(sodaBox);
                        }
                        break; 
                }
                
                // Return dequeued bottle
                return currentBottle;
            } 
            finally {
                Monitor.Exit(boxOfBottles);
            }

            Thread.Sleep(500);
        }
    }
}
