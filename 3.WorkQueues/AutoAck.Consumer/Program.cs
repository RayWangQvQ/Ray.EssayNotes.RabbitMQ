using System;
using Ray.EssayNotes.RabbitMQ.TaskQueues.Consumers;
using Ray.EssayNotes.RabbitMQ.TaskQueues.Producers;

namespace AutoAck.Consumer
{
    class Program
    {
        private static string QueueName = "task_auto_ack_queue";

        static void Main(string[] args)
        {
            //创建一个自动确认消费的消费者
            BaseConsumer consumer = new AutoAckConsumer(QueueName);
            consumer.ConsumeAndPrint();
            Console.ReadLine();
        }
    }
}
