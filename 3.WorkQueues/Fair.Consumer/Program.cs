using System;
using Ray.EssayNotes.RabbitMQ.TaskQueues.Consumers;

namespace Fair.Consumer
{
    class Program
    {
        private static string QueueName = "task_fair_dispatch_queue";

        static void Main(string[] args)
        {
            //BaseConsumer consumer = new AutoAckConsumer(QueueName);
            BaseConsumer consumer = new FairConsumer(QueueName);
            consumer.ConsumeAndPrint();
            Console.ReadLine();
        }
    }
}
/*
 * 在消费前，设置prefetchCount=1
 */
