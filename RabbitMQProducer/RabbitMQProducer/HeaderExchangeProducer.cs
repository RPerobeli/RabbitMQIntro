using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQProducer
{
    public static class HeaderExchangeProducer
    {
        public static void Publish(IModel channel)
        {
            var timeToLeave = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };
            channel.ExchangeDeclare("demo-exchange-header", ExchangeType.Headers, arguments: timeToLeave);
            int count = 0;
            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object>() { { "account", "new" } };
                channel.BasicPublish("demo-exchange-header", String.Empty, properties, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
