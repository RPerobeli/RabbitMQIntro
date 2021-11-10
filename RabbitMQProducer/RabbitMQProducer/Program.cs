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
                //QueueProducer.Publish(channel); 
                DirectExchangePublisher.Publish(channel); 
            }
            

        }
    }
}
