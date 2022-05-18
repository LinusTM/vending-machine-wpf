using System.Diagnostics;

namespace Flaske_Automat;

internal class Producer {
    internal static void Produce(Queue<Bottle> boxOfBottles) {
        while(true) {
            Monitor.Enter(boxOfBottles);
            try {
                // Check/wait if boxOfBottles is full
                while(boxOfBottles.Count == 10) {
                    Thread.Sleep(100/15);
                    Monitor.Wait(boxOfBottles);
                }

                // Initiate new beer or soda depending on random number
                switch(Random.Shared.Next(0, 2)) {
                    case 0:
                        Bottle beer = new Beer();
                        boxOfBottles.Enqueue(beer);
                        
                        Debug.WriteLine($"PRODUCER: Has produced {beer.Type}{beer.Number}");

                        break;
                    default:
                        Bottle soda = new Soda();
                        boxOfBottles.Enqueue(soda);

                        Debug.WriteLine($"PRODUCER: Has produced {soda.Type}{soda.Number}");

                        break;
                }

                // Notify threads: Update to boxOfBottles queue
                Monitor.PulseAll(boxOfBottles);
            }
            finally {
                Monitor.Exit(boxOfBottles);
            }

            Thread.Sleep(1000);
        }
    }
}
