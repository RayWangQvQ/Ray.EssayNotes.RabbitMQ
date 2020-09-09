﻿using System;
using Ray.EssayNotes.RabbitMQ.TaskQueues.Consumers;

namespace FullDurable.Consumer
{
    class Program
    {
        private static string QueueName = "full_durable_queue";

        static void Main(string[] args)
        {
            BaseConsumer consumer = new AckConsumer(QueueName);
            consumer.ConsumeAndPrint();
            Console.ReadLine();
        }
    }
}
