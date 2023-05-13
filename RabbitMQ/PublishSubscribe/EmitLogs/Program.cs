using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "localhost" };

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

while (true)
{
    Console.Write("Your message: ");
    var message = Console.ReadLine();

    if (message == null)
    {
        Console.WriteLine("Please write a message!");
        continue;
    }

    var body = Encoding.UTF8.GetBytes(message);

    channel.BasicPublish(
        exchange: "logs",
        routingKey: string.Empty,
        basicProperties: null,
        body: body);

    Console.WriteLine($"--- Sender sent message: {message}");
}