using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Ray.Infrastructure.Test;

namespace Ray.EssayNotes.RabbitMQ.Worker
{
    public class Test1 : ITest
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
                                autoAck: true,
                                consumer: consumer);

            /*
             * autoAck设置为true，表示只要RabbitMQ发送了消息就立即自动将队列中的消息设置为以消费
             * 此种会发生消费者接受到消息后出现异常后，消息就彻底丢失了
             */
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("接受到： {0}", message);

            //按照点的数量，阻塞相应秒数
            int dots = message.Split('.').Length - 1;
            Thread.Sleep(dots * 1000);

            Console.WriteLine("处理结束");
        }

        /*
         * 当多个消费者一起消费同一个queue时，RabbitMQ默认采用Round-robin分发模式，即轮转模式
         * 比如，当有A、B两个消费者时，RabbitMQ会依次向A发送、向B发送、向A发送......
         * 优点：从数量上相对平均，即能保证每个消费者最后消费掉的消息数量接近相等
         * 缺点：当A正在处理长时操作，而B空闲时，可能会导致RabbitMQ阻塞来等待向A发送
         */
    }
}
