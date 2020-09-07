using System;
using System.Text;
using RabbitMQ.Client;

namespace Ray.EssayNotes.RabbitMQ.NewTask
{
    /// <summary>
    /// 生产者
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "task_queue",
                     durable: true,//将是否持久化设置为true
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

                //todo
                IBasicProperties properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                string message = "";
                while (message != "quit")
                {
                    Console.WriteLine("\r\n请输入欲发送的消息（.的数量对应处理详细时阻塞的秒数）：");
                    message = Console.ReadLine();
                    byte[] body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "",
                                 routingKey: "task_queue",
                                 basicProperties: properties,
                                 body: body);

                    Console.WriteLine("发送成功");
                }
            }
        }
        /*
         * 开启手动确认ack后，可以保证消费者挂了之后，消息仍然会再次发送。
         * 但是当RabbitMQ挂了或重启之后，这些消息还是会遗失。
         * 我们可以通过设置durable，将消息持久化
         */
    }
}
