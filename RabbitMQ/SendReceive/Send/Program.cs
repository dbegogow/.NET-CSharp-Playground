using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "localhost" };

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare(
    queue: "hello",
    durable: false,
    exclusive: false,
    autoDelete: false,
    arguments: null);

while (true)
{
    var message = Console.ReadLine();

    if (message == null)
    {
        Console.WriteLine("Please write a message!");
        continue;
    }

    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(
        exchange: string.Empty,
        routingKey: "hello",
        basicProperties: null,
        body: body);

    Console.WriteLine($"--- Sender sent message: {message}");
}