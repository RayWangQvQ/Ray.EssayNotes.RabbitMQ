using System;
using System.Text;
using RabbitMQ.Client;

namespace PublishAndSubscribe.Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            //1.声明交换器
            channel.ExchangeDeclare(exchange: "logs",//交换器名称
                type: ExchangeType.Fanout);//Fanout模式：交换器向所有队列发送消息

            while (true)
            {
                var message = GetMessage(args);
                var body = Encoding.UTF8.GetBytes(message);

                //2.发布消息到交换器
                channel.BasicPublish(exchange: "logs",//指定交换器
                    routingKey: "",
                    basicProperties: null,
                    body: body);

                Console.WriteLine(" [x] Sent {0}", message);
            }
        }

        private static string GetMessage(string[] args)
        {
            Console.WriteLine("");
            return Console.ReadLine();
        }
    }
}
