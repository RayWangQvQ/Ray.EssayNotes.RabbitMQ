using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace PublishAndSubscribe.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            //1.声明交换机
            channel.ExchangeDeclare(exchange: "logs",
                type: ExchangeType.Fanout);//Fanout模式会向所有队列发送

            //2.声明队列
            var queueName = channel.QueueDeclare()//这里不写任何参数，会创建一个非持久化的（non-durable）、独有的（exclusive）、自动删除的（autodelete）队列，其名称会随机自动生成，形如（amq.gen-JzTY20BRgKO-HjmUJj0wLg）
                .QueueName;

            //3.绑定（将交换机与队列绑定）
            channel.QueueBind(queue: queueName,
                exchange: "logs",
                routingKey: "");//因为Fanout是发向所有队列，所以这里不需要指定路由了

            Console.WriteLine(" [*] Waiting for logs.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(" [x] {0}", message);
            };
            channel.BasicConsume(queue: queueName,
                autoAck: true,
                consumer: consumer);

            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
