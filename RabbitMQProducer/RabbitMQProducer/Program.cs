using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Threading;

namespace RabbitMQProducer
{
    static class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                var factory = new ConnectionFactory
                {
                    Uri = new Uri("amqp://guest:guest@localhost:5672")
                };
                using var connection = factory.CreateConnection();
                using var channel = connection.CreateModel();
                channel.QueueDeclare("demo-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);
                var message = new { Name = "Producer", Message = "Hello!" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                channel.BasicPublish("", "demo-queue", null, body);
                Thread.Sleep(2000);
            }
            

        }
    }
}
