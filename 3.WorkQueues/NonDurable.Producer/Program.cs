using System;
using Ray.EssayNotes.RabbitMQ.TaskQueues.Producers;

namespace NonDurable.Producer
{
    class Program
    {
        private static string QueueName = "non_durable_queue";

        static void Main(string[] args)
        {
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

/*
 * 这里队列和消息均不持久化
 * 模拟发送一条消费异常的消息（没有ack），然后关闭生产者和消费者，重启RabbitMQ服务
 * 结果：队列和消失均消失
 * p.s.重启之前需要关闭生产者和消费者，否则它们会自动新建队列
 */
