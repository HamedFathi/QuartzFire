```cs
class Program
{
    async static Task Main(string[] args)
    {

        await QuartzScheduler.Enqueue(() => Console.WriteLine(DateTime.Now), true);

        await QuartzScheduler.Enqueue<Email>(x => Console.WriteLine(x.Address + " " + DateTime.Now), true);

        await QuartzScheduler.Delay(() => Console.WriteLine(DateTime.Now), TimeSpan.FromSeconds(3), true);

        await QuartzScheduler.Delay<Email>(x => Console.WriteLine(x.Address + " " + DateTime.Now), TimeSpan.FromSeconds(5), true);

        await QuartzScheduler.Schedule(() => Console.WriteLine(DateTime.Now), TimeSpan.FromSeconds(7), TimeSpan.FromSeconds(1), true);

        await QuartzScheduler.Schedule(() => Console.WriteLine(DateTime.Now), 7, 1);

        await QuartzScheduler.Schedule(() => Console.WriteLine(DateTime.Now), "0 0/5 * * * ?");

        await QuartzScheduler.Schedule(() => Console.WriteLine("With TriggerBuilder"),
                builder => builder.StartNow()
                                  .WithSimpleSchedule(x => x
                                  .WithIntervalInSeconds(10)
                                  .RepeatForever()));
    }
}
```