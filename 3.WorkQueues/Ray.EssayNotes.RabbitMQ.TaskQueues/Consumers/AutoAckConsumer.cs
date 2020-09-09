using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Ray.EssayNotes.RabbitMQ.TaskQueues.Consumers
{
    public class AutoAckConsumer : BaseConsumer
    {
        public AutoAckConsumer(string queueName) : base(queueName)
        {

        }

        protected override void StartBasicConsume(IBasicConsumer basicConsumer)
        {
            Channel.BasicConsume(queue: QueueName,
                                autoAck: true,//设置为true，自动确认消费
                                consumer: basicConsumer);
        }
    }
}
