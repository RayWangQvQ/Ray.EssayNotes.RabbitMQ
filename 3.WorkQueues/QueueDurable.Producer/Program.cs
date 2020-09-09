using System;
using Ray.EssayNotes.RabbitMQ.TaskQueues.Producers;

namespace QueueDurable.Producer
{
    class Program
    {
        private static string QueueName = "queue_durable_queue";

        static void Main(string[] args)
        {
            BaseProducer producer = new QueueDurableProducer(QueueName);

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
 * 队列持久化，但是消息没有持久化
 * 重启后，队列仍存在，但是消息则全部丢失
 */
