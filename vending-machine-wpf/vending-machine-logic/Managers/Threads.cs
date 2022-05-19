using System.Collections.Generic;
using System.Threading;
using vending_machine_wpf.EventArgs;

namespace vending_machine_wpf.Managers;

public class Thread_Manager {
    // Shared resources
    static Queue<Bottle> boxOfBottles = new Queue<Bottle>();
    static Queue<Bottle> beerBox = new Queue<Bottle>();
    static Queue<Bottle> sodaBox = new Queue<Bottle>();
    
    public void StartThreads()
    {
        // Get thread functionality
        Producer producer = new Producer();
        Sorter sorter = new Sorter();
        Consumer sodaConsumer = new Consumer();
        Consumer beerConsumer = new Consumer();

        sodaConsumer.consumeEvent += OnSodaConsume;
        beerConsumer.consumeEvent += OnBeerConsume;
        
        // Creating, starting and stopping threads
        Thread produceBottles = new Thread(() => producer.Produce(boxOfBottles));
        Thread sortBottles = new Thread(() => sorter.Sort(boxOfBottles, beerBox, sodaBox));
        Thread consumeBeer = new Thread(() => beerConsumer.Consume(beerBox));
        Thread consumeSoda = new Thread(() => sodaConsumer.Consume(sodaBox));
        
        produceBottles.Start();
        sortBottles.Start();
        consumeBeer.Start();
        consumeSoda.Start();

        produceBottles.Join();
        sortBottles.Join();
        consumeBeer.Join();
        consumeSoda.Join();
    }

    public void OnSodaBufferUpdate(object? sender, BufferEventArgs e)
    {
        
    }
    
    public void OnBeerBufferUpdate(object? sender, BufferEventArgs e)
    {
        
    }
    
    public void OnSodaConsume(object? sender, BottleEventArgs e)
    {
        
    }
    
    public void OnBeerConsume(object? sender, BottleEventArgs e)
    {
        
    }

    public void PauseThreads()
    {
        
    }
}