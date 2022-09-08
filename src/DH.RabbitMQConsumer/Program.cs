using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


var factory = new ConnectionFactory { HostName = "rabbitmq" };
var connection = factory.CreateConnection();
using var channel = connection.CreateModel();

channel.QueueDeclare("posts", exclusive: false);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, eventargs) =>
{
    var body = eventargs.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine(message);
};

channel.BasicConsume(queue: "posts", autoAck: true, consumer: consumer);
Console.ReadKey();

