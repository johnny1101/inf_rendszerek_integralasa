using System;
using System.Threading.Tasks;
using Color.MessageLib;
using Color.MessageLib.Implementations;
using Color.MessagesLib;
using RabbitMQ.Client;

namespace ColorQueueProducer
{
    class Program
    {
        private static Random _random = new Random();
        public static int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        static void Main(string[] args)
        {
            var colourMessages = new string[] { MessageTypes.MessageRed, MessageTypes.MessageBlue, MessageTypes.MessageGreen };
            var messageService = new MessageService(
                new MessageServiceConfiguration
                {
                    Hostname = "localhost",
                    Port = AmqpTcpEndpoint.UseDefaultPort,
                    UserName = ConnectionFactory.DefaultUser,
                    Password = ConnectionFactory.DefaultPass,
                    QueueName = "colorQueue"
                });

            messageService.Init();

            Console.WriteLine("Start message sending...");

            while (true)
            {
                var randomIndex = RandomNumber(0, 3);

                Console.WriteLine($"Send message: {colourMessages[randomIndex]}");

                messageService.Enqueue(colourMessages[randomIndex]);

                Task.Delay(1000).Wait();
            }
        }
    }
}
