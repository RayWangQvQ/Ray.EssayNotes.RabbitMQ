using System;
using Ray.EssayNotes.RabbitMQ.TaskQueues.Consumers;

namespace MessageDurable.Consumer
{
    class Program
    {
        private static string QueueName = "message_durable_queue";

        static void Main(string[] args)
        {
            BaseConsumer consumer = new AckConsumer(QueueName);
            consumer.ConsumeAndPrint();
            Console.ReadLine();
        }
    }
}
