using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQ.Consumer
{
    public static class HeaderExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("demo-exchange-header", ExchangeType.Headers);
            channel.QueueDeclare("demo-header-queue", durable: true, exclusive: false, autoDelete: false, arguments: null);

            var header = new Dictionary<string, object>() { {"account","new" } };

            channel.QueueBind("demo-header-queue", "demo-exchange-header", String.Empty, header);
            channel.BasicQos(0, 10, false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, evento) =>
            {
                var body = evento.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            channel.BasicConsume("demo-header-queue", true, consumer);
            Console.WriteLine("Consumer Started");
            Console.ReadLine();
        }
    }
}
