using System;
using System.Text;
using System.Threading;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Ray.Infrastructure.Test;

namespace Ray.EssayNotes.RabbitMQ.Worker
{
    /// <summary>
    /// 消费者，在此案例中，可以启动多个Worker测试
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            ITest test =
                //new Test1();
                new Test2();
            test.Run();
        }
    }
}
