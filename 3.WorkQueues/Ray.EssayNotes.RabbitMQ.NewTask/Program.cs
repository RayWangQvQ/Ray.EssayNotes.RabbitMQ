using System;
using System.Text;
using RabbitMQ.Client;

namespace Ray.EssayNotes.RabbitMQ.NewTask
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            while (true)
            {
                Console.WriteLine("\r\n请输入欲等待秒数：");
                string message = Console.ReadLine();
                bool suc = int.TryParse(message, out int seconds);
                if (!suc) continue;
                byte[] body = Encoding.UTF8.GetBytes(message);

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    IBasicProperties properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish(exchange: "",
                                     routingKey: "task_queue",
                                     basicProperties: properties,
                                     body: body);
                    Console.WriteLine("发送成功");
                }


            }


        }
    }
}
