using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Ray.Infrastructure.Extensions.Json;

namespace Ray.EssayNotes.RabbitMQ.Receiver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("接受消息中......");

            //1.创建连接
            var factory = new ConnectionFactory { HostName = "localhost" };
            using (IConnection connection = factory.CreateConnection())
            //2.创建IModel
            using (IModel channel = connection.CreateModel())
            {
                //queue放在发送程序中初始化了，如果是先运行了该接受程序，则需要在这里和发送程序一样，也声明下名称为hello的queue

                //3.设置消费
                StartBasicConsume(channel);

                Console.ReadLine();
            }
        }

        private static void StartBasicConsume(IModel channel)
        {
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume(queue: "hello",
                                     autoAck: true,//acknowledgment，是否自动确认已被消费，设置为true时，RabbitMQ发送完消息后就立即将队列中的消息设置为已被消费（在3.workqueue中具体测试）
                                     consumer: consumer);
        }

        /// <summary>
        /// 每次接受到消息后触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private static void Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
        {
            Console.WriteLine($"接收到的BasicDeliverEventArgs：{eventArgs.AsFormatJsonStr(false)}");

            byte[] body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine("接受到body消息： {0}", message);
        }
    }
}
