using RPCClient;

Console.Write("Number: ");
var number = Console.ReadLine();

using var rpcClient = new RpcClient();

var response = await rpcClient.CallAsync(number);

Console.WriteLine($"Got: {response}");