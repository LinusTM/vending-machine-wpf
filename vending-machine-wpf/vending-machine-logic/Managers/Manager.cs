using System;
using vending_machine_wpf.EventArgs;

namespace vending_machine_wpf.Managers;

public class Manager
{
    internal event EventHandler<BottleEventArgs> bottleEvent;
    
    // Triggers event, when change to bottle occurs
    public void SodaConsumed(Bottle bottle)
    {
        bottleEvent?.Invoke(this, new BottleEventArgs(bottle));
    }
    
    public void BeerConsumed(Bottle bottle)
    {
        bottleEvent?.Invoke(this, new BottleEventArgs(bottle));
    }
    
    public void BottleDequeue(Bottle bottle)
    {
        bottleEvent?.Invoke(this, new BottleEventArgs(bottle));
    }
}