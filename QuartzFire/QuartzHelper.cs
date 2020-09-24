using System;

namespace QuartzFire
{
    // Calling methods in background
    // BackgroundJob.Enqueue<IEmailSender>(x => x.Send("hangfire@example.com"));
    // BackgroundJob.Enqueue(() => Console.WriteLine("Hello, world!"));

    // Calling methods with delay
    // BackgroundJob.Schedule(() => Console.WriteLine("Hello, world"), TimeSpan.FromDays(1));

    // Performing recurrent tasks
    // RecurringJob.AddOrUpdate(() => Console.Write("Easy!"), Cron.Daily);
    // RecurringJob.AddOrUpdate(() => Console.Write("Powerful!"), "0 12 * */2");
    // RecurringJob.AddOrUpdate("some-id", () => Console.WriteLine(), Cron.Hourly);
    // RecurringJob.RemoveIfExists("some-id");
    // RecurringJob.Trigger("some-id");

    // Using Batches
    // BatchJob.StartNew(x =>
    // {
    //     for (var i = 0; i < 1000; i++)
    //     {
    //         x.Enqueue(() => SendEmail(i));
    //     }
    // });

    public static class QuartzHelper
    {

    }
}
