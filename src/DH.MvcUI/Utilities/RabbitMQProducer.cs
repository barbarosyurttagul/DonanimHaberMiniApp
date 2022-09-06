using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace DH.MvcUI.Utilities
{
    public class RabbitMQProducer : IMessageProducer
    {
        public void SendPostMessage<T>(T message)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare("posts", exclusive: false);
            var json = JsonConvert.SerializeObject(message);
            var body = Encoding.UTF8.GetBytes(json);

            channel.BasicPublish(exchange: "", routingKey: "posts", body: body);
        }
    }
}

