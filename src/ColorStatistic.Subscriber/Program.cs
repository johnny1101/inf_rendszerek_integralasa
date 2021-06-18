using System;
using System.Text;
using Color.MessageLib.Implementations;

namespace ColorStatistic.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start colorStatistic listening.");

            var colorStatisticSubscriber = new SubscriberService("localhost", "colorStatistics");
            colorStatisticSubscriber.Init().Subscribe((model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"10 {message} messages has been processed.");
            });

            while (true) { }
        }
    }
}
