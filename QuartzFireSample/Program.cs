using Quartz;
using Quartz.Impl;
using QuartzFire;
using System;
using System.Threading.Tasks;

namespace QuartzFireSample
{
    public class Email
    {
        public string Address { get; set; } = "asdasdasd";
    }

    class Program
    {
        async static Task Main(string[] args)
        {
            // await BackgroundJob.Enqueue(() => Console.WriteLine( "asd"), false);

            // await BackgroundJob.Enqueue<Email>(x => Console.WriteLine(x.Address + "as222d"), true);

            // await QuartzHelper.Schedule(() => Console.WriteLine("Hello"),TimeSpan.FromSeconds(1),TimeSpan.FromSeconds(4));

            Console.ReadKey();
        }
    }
}
