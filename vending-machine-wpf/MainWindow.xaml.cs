using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using vending_machine_wpf.EventArgs;

namespace vending_machine_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Shared resources
        static Queue<Bottle> boxOfBottles = new Queue<Bottle>();
        static Queue<Bottle> beerBox = new Queue<Bottle>();
        static Queue<Bottle> sodaBox = new Queue<Bottle>();
        
        public MainWindow()
        {
            InitializeComponent();
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
        
            sodaConsumer.consumeEvent += OnSodaConsume;
            beerConsumer.consumeEvent += OnBeerConsume;
        }
        public void OnSodaBufferUpdate(object? sender, BufferEventArgs e)
        {
            Dispatcher.Invoke(() => { SodaBuffer.Content = e.Bottles.Count.ToString(); });
        }

        public void OnBeerBufferUpdate(object? sender, BufferEventArgs e)
        {
            Dispatcher.Invoke(() => { BeerBuffer.Content = e.Bottles.Count.ToString(); });
        }

        public void OnSodaConsume(object? sender, BottleEventArgs e)
        {
        }
        
        public void OnBeerConsume(object? sender, BottleEventArgs e)
        {
        }
        
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }
    }
}
