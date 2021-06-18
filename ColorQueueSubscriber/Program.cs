using System;
using System.Text;
using Color.MessageLib;
using Color.MessageLib.Implementations;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ColorQueueSubscriber
{

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create connection with RabbitMQ.");

            var messageService = new ColorMessageService(
                new MessageServiceConfiguration
                {
                    Hostname = "localhost",
                    Port = AmqpTcpEndpoint.UseDefaultPort,
                    UserName = ConnectionFactory.DefaultUser,
                    Password = ConnectionFactory.DefaultPass,
                    QueueName = "colorStatistics"
                });

            messageService.Init();

            var subscriberService = new SubscriberService("localhost", "colorQueue");
            subscriberService
                .Init()
                .Subscribe((object mowdel, BasicDeliverEventArgs ea) => {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    messageService.EnqueueColorMessage(message);

                    Console.WriteLine("Message Received.");
                });

            Console.WriteLine("Start colorQueue listening.");

            Console.ReadKey();
        }
    }
}
