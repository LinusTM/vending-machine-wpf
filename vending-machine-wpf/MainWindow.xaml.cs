using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using vending_machine_wpf.EventArgs;

namespace vending_machine_wpf;

public partial class MainWindow : Window {
    // Shared resources
    static readonly Queue<Bottle> boxOfBottles = new();
    static readonly Queue<Bottle> beerBox = new();
    static readonly Queue<Bottle> sodaBox = new();

    public MainWindow() {
        InitializeComponent();

        Producer producer = new Producer();
        Sorter sorter = new Sorter();
        Consumer sodaConsumer = new Consumer();
        Consumer beerConsumer = new Consumer();

        // Bind events to methods
        sodaConsumer.consumeEvent += OnSodaConsume;
        beerConsumer.consumeEvent += OnBeerConsume;
        sorter.sodaBufferEvent += OnSodaBufferUpdate;
        sorter.beerBufferEvent += OnBeerBufferUpdate;
        sorter.sortEvent += OnSort;

        // Creating and starting threads
        Thread produceBottles = new Thread(() => producer.Produce(boxOfBottles));
        Thread sortBottles = new Thread(() => sorter.Sort(boxOfBottles, beerBox, sodaBox));
        Thread consumeBeer = new Thread(() => beerConsumer.Consume(beerBox));
        Thread consumeSoda = new Thread(() => sodaConsumer.Consume(sodaBox));

        produceBottles.Start();
        sortBottles.Start();
        consumeBeer.Start();
        consumeSoda.Start();
    }

    private void OnSodaBufferUpdate(object? sender, BufferEventArgs e) {
        Dispatcher.Invoke(() => { SodaBuffer.Content = e.Bottles.Count.ToString(); });
    }

    private void OnBeerBufferUpdate(object? sender, BufferEventArgs e) {
        Dispatcher.Invoke(() => { BeerBuffer.Content = e.Bottles.Count.ToString(); });
    }

    private void OnSodaConsume(object? sender, BottleEventArgs e) {
        Dispatcher.Invoke(() => { ConsumedBeer.Content = e.Bottle.Type + e.Bottle.Number; });
    }

    private void OnSort(object? sender, BottleEventArgs e) {
        Dispatcher.Invoke(() => { SortedBottle.Content = e.Bottle.Type + e.Bottle.Number; });
    }
    
    private void OnBeerConsume(object? sender, BottleEventArgs e) {
        Dispatcher.Invoke(() => { ConsumedSoda.Content = e.Bottle.Type + e.Bottle.Number; });
    }

    private void Grid_MouseDown(object sender, MouseButtonEventArgs e) {
    }
}