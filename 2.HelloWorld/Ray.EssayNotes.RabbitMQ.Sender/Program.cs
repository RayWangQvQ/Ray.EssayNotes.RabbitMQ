using System;
using System.Text;
using RabbitMQ.Client;

namespace Ray.EssayNotes.RabbitMQ.Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            while (true)
            {
                Console.WriteLine("\r\n请输入欲发送的消息：");
                string message = Console.ReadLine();
                byte[] body = Encoding.UTF8.GetBytes(message);

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine("发送成功");
                }
            }


        }
    }
}
