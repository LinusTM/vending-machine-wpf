namespace vending_machine_wpf.EventArgs;

public class BottleEventArgs : System.EventArgs {
    public Bottle Bottle { get; set; }

    public BottleEventArgs(Bottle bottle) {
        Bottle = bottle;
    }
}