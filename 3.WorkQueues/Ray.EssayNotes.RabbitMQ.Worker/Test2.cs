using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Ray.Infrastructure.Test;

namespace Ray.EssayNotes.RabbitMQ.Worker
{
    public class Test2 : ITest
    {
        public void Run()
        {
            Console.WriteLine("接受消息中......");

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                StartBasicConsume(channel);

                Console.ReadLine();
            }
        }

        private void StartBasicConsume(IModel channel)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume(queue: "task_queue",
                                autoAck: false,
                                consumer: consumer);
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("接受到： {0}", message);

            int dots = message.Split('.').Length - 1;
            Thread.Sleep(dots * 1000);

            //假装发生异常
            if (message.Contains("1"))
            {
                throw new Exception();
            }

            //手动回调，告诉RabbitMQ消费成功
            ((EventingBasicConsumer)sender).Model.BasicAck(deliveryTag: eventArgs.DeliveryTag, multiple: false);

            Console.WriteLine("处理结束");
        }

        /*
         * 
         */
    }
}
