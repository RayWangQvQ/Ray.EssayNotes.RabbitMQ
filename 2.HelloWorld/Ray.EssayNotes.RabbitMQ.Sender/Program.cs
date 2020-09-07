using System;
using System.Text;
using RabbitMQ.Client;

namespace Ray.EssayNotes.RabbitMQ.Sender
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            //1.创建连接（ socket 连接）
            using (IConnection connection = factory.CreateConnection())
            //2.创建IModel（大部分api都封装在IModel里）
            using (IModel channel = connection.CreateModel())
            {
                //3.声明一个队列，如果不存在，则会创建一个。消息只能发到queue里。
                channel.QueueDeclare(queue: "hello",//队列名称
                                     durable: false,//是否持久化，队列的声明默认是存放到内存中的，如果rabbitmq重启会丢失，如果想重启之后还存在就要使队列持久化，保存到Erlang自带的Mnesia数据库中，当rabbitmq重启之后会读取该数据库
                                     exclusive: false,//是否排外的，有两个作用，一：当连接关闭时connection.close()该队列是否会自动删除；二：该队列是否是私有的private，如果不是排外的，可以使用两个消费者都访问同一个队列，没有任何问题，如果是排外的，会对当前队列加锁，其他通道channel是不能访问的，如果强制访问会报异常，一般等于true的话用于一个队列只能有一个消费者来消费的场景
                                     autoDelete: false,//队列是否自动删除，当最后一个消费者断开连接之后队列是否自动被删除
                                     arguments: null);

                string message = "";
                while (message != "quit")
                {
                    //4.将消息转化为byte
                    message = GetMessage();
                    byte[] body = Encoding.UTF8.GetBytes(message);

                    //5.发布到queue
                    channel.BasicPublish(exchange: "",
                                         routingKey: "hello",
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine("发送成功");
                }
            }
        }

        private static string GetMessage()
        {
            Console.WriteLine("\r\n请输入欲发送的消息：");
            string message = Console.ReadLine();
            return message;
        }
    }
}
