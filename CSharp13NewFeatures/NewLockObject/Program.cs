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

public class LockExample
{
    private readonly Lock _lock = new();
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public async Task DoStuff(int val)
    {
        lock (_lock)
        {
            // await Task.Delay(1000); - Compiler Error: Cannot 'await' in the body of a 'lock' statement
        }

        using (_lock.EnterScope())
        {
            await Task.Delay(1000); // Runtime Error: Instance of type 'System.Threading.Lock.Scope' cannot be preserved across 'await' or 'yield' boundary.
        }

        await _semaphore.WaitAsync();
        try
        {
            await Task.Delay(10); // No Errors
        }
        finally
        {
            _semaphore.Release();
        }
    }
}