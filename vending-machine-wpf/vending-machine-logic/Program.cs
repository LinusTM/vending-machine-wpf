namespace Flaske_Automat;

class Program {
    // Shared resources
    static Queue<Bottle> boxOfBottles = new Queue<Bottle>();
    static Queue<Bottle> beerBox = new Queue<Bottle>();
    static Queue<Bottle> sodaBox = new Queue<Bottle>();

    static void Main(string[] args) {
        // Creating, starting and stopping threads
        Thread produceBottles = new Thread(() => Producer.Produce(boxOfBottles));
        Thread sortBottles = new Thread(() => Sorter.Sort(boxOfBottles, beerBox, sodaBox));
        Thread consumeBeer = new Thread(() => Consumer.Consume(beerBox));
        Thread consumeSoda = new Thread(() => Consumer.Consume(sodaBox));

        produceBottles.Start();
        sortBottles.Start();
        consumeBeer.Start();
        consumeSoda.Start();

        produceBottles.Join();
        sortBottles.Join();
        consumeBeer.Join();
        consumeSoda.Join();
    }
}
