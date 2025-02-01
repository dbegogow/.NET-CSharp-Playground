var guid = Guid.NewGuid(); // v4 UUID
Console.WriteLine(guid);

guid = Guid.CreateVersion7(); // v7 UUID
Console.WriteLine(guid);

Task.Delay(10000).Wait();

guid = Guid.CreateVersion7(TimeProvider.System.GetUtcNow()); // v7 UUID with timestamp
Console.WriteLine(guid);