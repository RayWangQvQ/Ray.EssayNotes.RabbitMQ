using System;
using Ray.EssayNotes.RabbitMQ.TaskQueues.Consumers;

namespace Ack.Consumer
{
    class Program
    {
        private static string QueueName = "task_ack_queue";

        static void Main(string[] args)
        {
            BaseConsumer consumer = new AckConsumer(QueueName);
            consumer.ConsumeAndPrint();
            Console.ReadLine();
        }
    }
}
