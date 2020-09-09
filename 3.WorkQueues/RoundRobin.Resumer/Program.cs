using System;
using Ray.EssayNotes.RabbitMQ.TaskQueues.Consumers;

namespace RoundRobin.Consumer
{
    class Program
    {
        private static string QueueName = "task_round_robin_queue";

        static void Main(string[] args)
        {
            //BaseConsumer consumer = new AutoAckConsumer(QueueName);
            BaseConsumer consumer = new AckConsumer(QueueName);
            consumer.ConsumeAndPrint();
            Console.ReadLine();
        }
    }
}
/*
 * 需开启多个消费者，然后发送消息
 * RabbitMQ默认使用轮转分发模式，即如果有A、B两个消费者，则依次向A发、然后向B发、然后再向A发送......
 * 如果A正在处理耗时任务，RabbitMQ不会考虑谁的压力大，依旧会轮转分发，
 * 所以，在极端情况下，会导致A收到的都是繁重任务，B收到的全都是轻松的任务，最后A会处理极慢甚至奔溃
 */
