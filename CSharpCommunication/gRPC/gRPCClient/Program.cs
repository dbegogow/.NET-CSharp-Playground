using Grpc.Net.Client;
using gRPCServer;

var chanel = GrpcChannel.ForAddress("https://localhost:7290");
var client = new Greeter.GreeterClient(chanel);

var newGreetingRequests = new List<GreetingRequest>
{
    new GreetingRequest {Name = "Ivana", Country = "France"},
    new GreetingRequest {Name = "Ivana", Country = "Italy"},
    new GreetingRequest {Name = "Dzhulio", Country = "Germany"},
};

foreach (var newGreeting in newGreetingRequests)
{
    var isAddedSuccessfuly = await client.SaveGreetingAsync(newGreeting);
    Console.WriteLine($"Saving new greeting status: {isAddedSuccessfuly.Summary}");
}

Console.WriteLine("---------------------");

var allGreetings = await client.GetGreetingsAsync(new Empty());
var allGreetingsMessages = allGreetings.Replies.Select(r => r.Message);

Console.WriteLine(string.Join(Environment.NewLine, allGreetingsMessages));