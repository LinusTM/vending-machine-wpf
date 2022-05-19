using System.Collections.Generic;
using System.Threading;

namespace vending_machine_wpf.Managers;

public class Thread_Manager {
    // Shared resources
    static Queue<Bottle> boxOfBottles = new Queue<Bottle>();
    static Queue<Bottle> beerBox = new Queue<Bottle>();
    static Queue<Bottle> sodaBox = new Queue<Bottle>();
    
    public void StartThreads()
    {
        // Alert observers that a change has occurred
        Manager manager = new Manager();
        
        // Creating, starting and stopping threads
        Thread produceBottles = new Thread(() => Producer.Produce(boxOfBottles));
        Thread sortBottles = new Thread(() => manager.BottleDequeue(Sorter.Sort(boxOfBottles, beerBox, sodaBox)));
        Thread consumeBeer = new Thread(() => manager.BeerConsumed(Consumer.Consume(beerBox)));
        Thread consumeSoda = new Thread(() => manager.SodaConsumed(Consumer.Consume(sodaBox)));
        
        produceBottles.Start();
        sortBottles.Start();
        consumeBeer.Start();
        consumeSoda.Start();

        produceBottles.Join();
        sortBottles.Join();
        consumeBeer.Join();
        consumeSoda.Join();
    }

    public void PauseThreads()
    {
        
    }
}