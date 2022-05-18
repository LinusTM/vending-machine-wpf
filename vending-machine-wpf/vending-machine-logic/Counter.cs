namespace vending_machine_wpf;

internal class Counter {
    private static int number;

    public static int Number { get => number; }

    public static void Increment() {
        number += 1;
    }
}
