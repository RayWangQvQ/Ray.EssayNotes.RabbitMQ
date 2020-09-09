using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace Ray.EssayNotes.RabbitMQ.TaskQueues.Producers
{
    public abstract class BaseProducer
    {
        protected IModel Channel { get; set; }

        public BaseProducer(string queueName)
        {
            QueueName = queueName;
            SetModel();
            QueueDeclare();
        }

        public virtual string QueueName { get; }

        public virtual void SetModel()
        {
            if (Channel != null) return;

            var factory = new ConnectionFactory() { HostName = "localhost" };
            IConnection connection = factory.CreateConnection();
            Channel = connection.CreateModel();
        }

        public abstract void QueueDeclare();

        public virtual void Publish(string message)
        {
            byte[] body = Encoding.UTF8.GetBytes(message);
            Channel.BasicPublish(exchange: "",
                         routingKey: QueueName,
                         basicProperties: GetBasicProperties(),
                         body: body);
            Console.WriteLine("发送成功");
        }

        protected virtual IBasicProperties GetBasicProperties()
        {
            return null;
        }
    }
}
