using System;
using Ray.EssayNotes.RabbitMQ.TaskQueues.Consumers;

namespace NonDurable.Consumer
{
    class Program
    {
        private static string QueueName = "non_durable_queue";

        static void Main(string[] args)
        {
            BaseConsumer consumer = new AckConsumer(QueueName);
            consumer.ConsumeAndPrint();
            Console.ReadLine();
        }
    }
}
