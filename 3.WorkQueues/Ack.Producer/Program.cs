using System;
using Ray.EssayNotes.RabbitMQ.TaskQueues.Producers;

namespace Ack.Producer
{
    class Program
    {
        private static string QueueName = "task_ack_queue";

        static void Main(string[] args)
        {
            BaseProducer producer = new NonDurableProducer(QueueName);

            while (true)
            {
                Console.WriteLine("\r\n请输入欲发送的消息（.的数量对应处理详细时阻塞的秒数）：");
                producer.Publish(Console.ReadLine());
                Console.WriteLine($"成功发送到{producer.QueueName}");
            }
        }
    }
}
