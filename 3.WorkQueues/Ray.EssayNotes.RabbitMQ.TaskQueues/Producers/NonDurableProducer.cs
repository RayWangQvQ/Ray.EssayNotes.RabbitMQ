using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace Ray.EssayNotes.RabbitMQ.TaskQueues.Producers
{
    public class NonDurableProducer : BaseProducer
    {
        public NonDurableProducer(string queueName) : base(queueName)
        {

        }

        public override void QueueDeclare()
        {
            this.Channel.QueueDeclare(queue: QueueName,
                     durable: false,//不持久化存储
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);
        }
    }
}
