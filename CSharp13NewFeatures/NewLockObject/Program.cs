public class LockExampleBefore
{
    private readonly object _lock = new();

    public void DoStuff()
    {
        lock (_lock)
        {
            Console.WriteLine("We're inside old lock");
        }
    }
}


public class LockExampleNow
{
    private readonly Lock _lock = new();

    public void DoStuff()
    {
        lock (_lock)
        {
            Console.WriteLine("We're inside new lock");
        }
    }
}