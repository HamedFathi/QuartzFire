using Quartz;
using Quartz.Impl;
using QuartzFire;
using System;
using System.Threading.Tasks;

namespace QuartzFireSample
{
    public class Email
    {
        public string Address { get; set; } = "myAddress";
    }

    class Program
    {
        async static Task Main(string[] args)
        {
            await BackgroundJob.Enqueue(() => Console.WriteLine(DateTime.Now), true);

            await BackgroundJob.Enqueue<Email>(x => Console.WriteLine(x.Address + " | " + DateTime.Now), true);

            await BackgroundJob.Delay(() => Console.WriteLine(DateTime.Now), TimeSpan.FromSeconds(3), true);

            await BackgroundJob.Delay<Email>(x => Console.WriteLine(x.Address + " | " + DateTime.Now), TimeSpan.FromSeconds(5), true);

            Console.ReadKey();
        }
    }
}
