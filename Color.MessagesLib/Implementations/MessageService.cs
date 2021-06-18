using System;
using System.Text;
using RabbitMQ.Client;
using Color.MessageLib.Interfaces;

namespace Color.MessageLib.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly MessageServiceConfiguration _configuration;

        private ConnectionFactory _factory;
        private IConnection _conn;
        private IModel _channel;        

        public MessageService(MessageServiceConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Init()
        {
            _factory = new ConnectionFactory
            {
                HostName = _configuration.Hostname,
                Port = _configuration.Port,
                UserName = _configuration.UserName,
                Password = _configuration.Password,
            };

            _conn = _factory.CreateConnection();
            _channel = _conn.CreateModel();
            _channel.QueueDeclare(queue: _configuration.QueueName,
                                    durable: false,
                                    exclusive: false,
                                    autoDelete: false,
                                    arguments: null);
        }

        public bool Enqueue(string messageString)
        {
            var body = Encoding.UTF8.GetBytes(messageString);
            _channel.BasicPublish(exchange: "",
                                routingKey: _configuration.QueueName,
                                basicProperties: null,
                                body: body);

            Console.WriteLine($"{messageString} message has published to { _configuration.QueueName} queue of the RabbitMQ.");

            return true;
        }
    }
}
