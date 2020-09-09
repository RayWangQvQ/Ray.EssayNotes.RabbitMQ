using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Ray.EssayNotes.RabbitMQ.TaskQueues.Consumers
{
    public abstract class BaseConsumer
    {
        protected IModel Channel { get; set; }

        public BaseConsumer(string queueName)
        {
            SetModel();
            QueueName = queueName;
        }

        public virtual string QueueName { get; }

        public virtual void SetModel()
        {
            if (Channel != null) return;

            var factory = new ConnectionFactory() { HostName = "localhost" };
            IConnection connection = factory.CreateConnection();
            Channel = connection.CreateModel();
        }

        public void ConsumeAndPrint()
        {
            var consumer = new EventingBasicConsumer(Channel);
            consumer.Received += Consumer_Received;

            StartBasicConsume(consumer);
        }

        protected abstract void StartBasicConsume(IBasicConsumer basicConsumer);

        protected virtual void Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("接受到： {0}", message);

            var contents = message.Split(',');
            message = contents[0];

            //如果字符串长度为偶数，则模拟消费失败
            if (message.Length % 2 == 0)
            {
                Console.WriteLine("消费失败");
                return;
            }

            //阻塞相应秒数
            string num = contents.Length > 1 ? contents[1] : "";
            int seconds = string.IsNullOrWhiteSpace(num) ? 0 : int.Parse(num);
            Thread.Sleep(seconds * 1000);

            Console.WriteLine("处理结束");

            AfterHandle(sender, eventArgs);
        }

        protected virtual void AfterHandle(object sender, BasicDeliverEventArgs eventArgs)
        {
            return;
        }
    }
}
