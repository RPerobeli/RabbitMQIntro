using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RabbitMQProducer
{
    public static class DirectExchangePublisher
    {
        public static void Publish(IModel channel)
        {
            var timeToLeave = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };
            channel.ExchangeDeclare("demo-exchange-direct", ExchangeType.Direct, arguments: timeToLeave);
            int count = 0;
            while (true)
            {
                var message = new { Name = "Producer", Message = $"Hello! Count: {count}" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
                channel.BasicPublish("demo-exchange-direct", "account.init", null, body);
                count++;
                Thread.Sleep(1000);
            }
        }
    }
}
