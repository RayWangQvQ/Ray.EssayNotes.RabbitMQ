using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Ray.EssayNotes.RabbitMQ.Worker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("接受消息中......");

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "task_queue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine("接受到： {0}", message);

                    int seconds = int.Parse(message);
                    Thread.Sleep(seconds * 1000);

                    Console.WriteLine("处理结束");
                };
                channel.BasicConsume(queue: "task_queue",
                                    autoAck: true,
                                    consumer: consumer);

                Console.ReadLine();
            }
        }
    }
}
