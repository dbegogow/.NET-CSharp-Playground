using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

var factory = new ConnectionFactory { HostName = "localhost" };

using var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

var queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(
    queue: queueName,
    exchange: "logs",
    routingKey: string.Empty);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Thread.Sleep(15000);

    Console.WriteLine($"Receive message: {message}");
};

channel.BasicConsume(
    queue: queueName,
    autoAck: true,
    consumer: consumer);

Console.WriteLine("Press [enter] to exit.");
Console.ReadLine();