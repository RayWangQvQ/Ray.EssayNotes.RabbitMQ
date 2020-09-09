using System;
using Ray.EssayNotes.RabbitMQ.TaskQueues.Consumers;

namespace QueueDurable.Consumer
{
    class Program
    {
        private static string QueueName = "queue_durable_queue";

        static void Main(string[] args)
        {
            BaseConsumer consumer = new AckConsumer(QueueName);
            consumer.ConsumeAndPrint();
            Console.ReadLine();
        }
    }
}
