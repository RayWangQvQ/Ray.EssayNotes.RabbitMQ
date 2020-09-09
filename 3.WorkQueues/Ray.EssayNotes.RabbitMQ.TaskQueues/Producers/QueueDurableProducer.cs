using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace Ray.EssayNotes.RabbitMQ.TaskQueues.Producers
{
    public class QueueDurableProducer : BaseProducer
    {
        public QueueDurableProducer(string queueName) : base(queueName)
        {

        }

        public override void QueueDeclare()
        {
            this.Channel.QueueDeclare(queue: QueueName,
                     durable: true,//开启
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);
        }
    }
}
