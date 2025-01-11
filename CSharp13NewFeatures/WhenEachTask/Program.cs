// List of 5 tasks that finish at random intervals
var tasks = Enumerable.Range(1, 5)
   .Select(async i =>
   {
       await Task.Delay(new Random().Next(1000, 5000));
       return $"Task {i} done";
   })
   .ToList();

// Before
while (tasks.Count > 0)
{
    var completedTask = await Task.WhenAny(tasks);
    tasks.Remove(completedTask);
    Console.WriteLine(await completedTask);
}

// .NET 9
await foreach (var completedTask in Task.WhenEach(tasks))
    Console.WriteLine(await completedTask);