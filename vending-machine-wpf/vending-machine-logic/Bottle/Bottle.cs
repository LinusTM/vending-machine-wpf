namespace vending_machine_wpf;

public abstract class Bottle : IBottle {
    private int number;
    private string type;

    public Bottle(string type) {
        Counter.Increment();

        this.number = Counter.Number;
        this.type = type;
    }

    public int Number { get => this.number; }
    public string Type { get => this.type; }
}
