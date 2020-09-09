using System;
using Ray.EssayNotes.RabbitMQ.TaskQueues.Producers;

namespace Fair.Producer
{
    class Program
    {
        private static string QueueName = "task_fair_dispatch_queue";

        static void Main(string[] args)
        {
            BaseProducer producer = new FullDurableProducer(QueueName);

            while (true)
            {
                Console.WriteLine("\r\n请输入欲发送的消息（格式为“{消息},{处理秒数}”）：");
                producer.Publish(Console.ReadLine());
                Console.WriteLine($"成功发送到{producer.QueueName}");
            }
        }
    }
}
