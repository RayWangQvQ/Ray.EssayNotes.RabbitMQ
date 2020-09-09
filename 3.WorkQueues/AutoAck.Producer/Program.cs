using System;
using Ray.EssayNotes.RabbitMQ.TaskQueues.Consumers;
using Ray.EssayNotes.RabbitMQ.TaskQueues.Producers;

namespace AutoAck.Producer
{
    class Program
    {
        private static string QueueName = "task_auto_ack_queue";

        static void Main(string[] args)
        {
            //创建一个生产者（这里只测试自动确认，生成者不做要求）
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
