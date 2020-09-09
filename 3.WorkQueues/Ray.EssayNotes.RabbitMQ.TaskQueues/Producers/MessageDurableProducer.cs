using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace Ray.EssayNotes.RabbitMQ.TaskQueues.Producers
{
    public class MessageDurableProducer : BaseProducer
    {
        public MessageDurableProducer(string queueName) : base(queueName)
        {

        }

        public override void QueueDeclare()
        {
            this.Channel.QueueDeclare(queue: QueueName,
                     durable: false,//关闭
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);
        }

        protected override IBasicProperties GetBasicProperties()
        {
            //设置消息持久化
            var properties = Channel.CreateBasicProperties();
            properties.Persistent = true;

            return properties;
        }
    }
}
