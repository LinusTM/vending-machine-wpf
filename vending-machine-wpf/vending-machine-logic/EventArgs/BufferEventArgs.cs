using System.Collections.Generic;

namespace vending_machine_wpf.EventArgs;

public class BufferEventArgs : System.EventArgs {
    public Queue<Bottle> Bottles { get; set; }

    public BufferEventArgs(Queue<Bottle> bottles) {
        Bottles = bottles;
    }
}