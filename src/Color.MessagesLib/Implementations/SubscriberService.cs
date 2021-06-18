using System;
using Color.MessageLib.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Color.MessageLib.Implementations
{
    public class SubscriberService : ISubscriberService
    {
        private readonly string _hostname;
        private readonly string _queue;
        private IModel _channel;

        public SubscriberService(string hostname, string queue)
        {
            _hostname = hostname;
            _queue = queue;
        }

        public ISubscriberService Init()
        {
            var factory = new ConnectionFactory
            {
                HostName = _hostname,
                Port = AmqpTcpEndpoint.UseDefaultPort,
                UserName = ConnectionFactory.DefaultUser,
                Password = ConnectionFactory.DefaultPass
            };
            var conn = factory.CreateConnection();
            _channel = conn.CreateModel();
            _channel.QueueDeclare(queue: _queue,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);

            return this;
        }

        public ISubscriberService Subscribe(EventHandler<BasicDeliverEventArgs> callback)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += callback;
            _channel.BasicConsume(queue: _queue, autoAck: true, consumer: consumer);

            return this;
        }
    }
}
