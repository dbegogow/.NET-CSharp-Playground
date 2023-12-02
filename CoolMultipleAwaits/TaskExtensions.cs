namespace CoolMultipleAwaits;

using System.Runtime.CompilerServices;

public static class TaskExtensions
{
    public static TaskAwaiter<T[]> GetAwaiter<T>(this (Task<T>, Task<T>) tasksTuple)
    {
        return Task.WhenAll(tasksTuple.Item1, tasksTuple.Item2).GetAwaiter();
    }
}
