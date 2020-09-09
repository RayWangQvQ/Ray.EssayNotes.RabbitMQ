using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Ray.EssayNotes.RabbitMQ.TaskQueues.Consumers
{
    public class AckConsumer : BaseConsumer
    {
        public AckConsumer(string queueName) : base(queueName)
        {

        }

        protected override void StartBasicConsume(IBasicConsumer basicConsumer)
        {
            Channel.BasicConsume(queue: QueueName,
                                autoAck: false,//设置为false
                                consumer: basicConsumer);
        }

        protected override void AfterHandle(object sender, BasicDeliverEventArgs eventArgs)
        {
            //手动回调，告诉RabbitMQ消费成功
            ((EventingBasicConsumer)sender).Model.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);
        }
    }
}
