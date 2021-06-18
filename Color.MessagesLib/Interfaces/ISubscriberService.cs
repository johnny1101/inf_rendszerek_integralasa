using System;
using RabbitMQ.Client.Events;

namespace Color.MessageLib.Interfaces
{
    public interface ISubscriberService
    {
        public ISubscriberService Init();
        public ISubscriberService Subscribe(EventHandler<BasicDeliverEventArgs> callback);
    }
}
