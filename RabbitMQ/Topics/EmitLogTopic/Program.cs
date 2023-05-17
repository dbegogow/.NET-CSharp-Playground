using System.Text;
using RabbitMQ.Client;

var factory = new ConnectionFactory { HostName = "localhost" };

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "topic_logs", type: ExchangeType.Topic);

Console.Write($"Routing key: ");
var routingKey = Console.ReadLine();

Console.WriteLine(new string('-', 50));

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
        exchange: "topic_logs",
        routingKey: routingKey,
        basicProperties: null,
        body: body);

    Console.WriteLine($"--- Sender sent message: {message}");
}