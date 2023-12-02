using RefReadonlyParameters;

var example = new Example();

var age = 30;

example.Test(ref age);

Console.WriteLine($"The age is: {age}");