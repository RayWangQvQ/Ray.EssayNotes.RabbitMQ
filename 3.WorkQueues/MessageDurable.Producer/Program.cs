using System;
using Ray.EssayNotes.RabbitMQ.TaskQueues.Producers;

namespace MessageDurable.Producer
{
    class Program
    {
        private static string QueueName = "message_durable_queue";

        static void Main(string[] args)
        {
            BaseProducer producer = new MessageDurableProducer(QueueName);

            while (true)
            {
                Console.WriteLine("\r\n请输入欲发送的消息（.的数量对应处理详细时阻塞的秒数）：");
                producer.Publish(Console.ReadLine());
                Console.WriteLine($"成功发送到{producer.QueueName}");
            }
        }
    }
}
/*
 * 消息持久化、队列不持久化，重启后队列丢失，消息也丢失
 */
