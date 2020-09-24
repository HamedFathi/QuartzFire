using System;
using System.Threading.Tasks;

namespace Quartz
{
    public static partial class QuartzLambdaExtentions
    {
        public static Task<DateTimeOffset> Delay(this IScheduler scheduler, Action action, TimeSpan delay, bool disallowConcurrentJob = false)
        {
            IJobDetail jobDetail;
            if (disallowConcurrentJob)
            {
                var data = new JobDataMap { { "DisallowConcurrentJobAction", action } };
                jobDetail = JobBuilder
                    .Create<DisallowConcurrentJob>()
                    .UsingJobData(data)
                    .Build();
            }
            else
            {
                var data = new JobDataMap { { "JobAction", action } };
                jobDetail = JobBuilder
                    .Create<Job>()
                    .UsingJobData(data)
                    .Build();
            }

            var date = DateTimeOffset.UtcNow.AddTicks(delay.Ticks);
            var trigger = TriggerBuilder.Create()
                .StartAt(date)
                .Build();

            return scheduler.ScheduleJob(jobDetail, trigger);
        }

        public static Task<DateTimeOffset> Delay<T>(this IScheduler scheduler, Action<T> action, TimeSpan delay, bool disallowConcurrentJob = false)
            where T : new()
        {
            IJobDetail jobDetail;
            if (disallowConcurrentJob)
            {
                var data = new JobDataMap { { "DisallowConcurrentJobWithTypeAction", action.Convert() }, { "DisallowConcurrentJobWithType", new T() } };
                jobDetail = JobBuilder
                    .Create<DisallowConcurrentJobWithType>()
                    .UsingJobData(data)
                    .Build();
            }
            else
            {
                var data = new JobDataMap { { "JobWithTypeAction", action.Convert() }, { "JobWithType", new T() } };
                jobDetail = JobBuilder
                    .Create<JobWithType>()
                    .UsingJobData(data)
                    .Build();
            }

            var date = DateTimeOffset.UtcNow.AddTicks(delay.Ticks);
            var trigger = TriggerBuilder.Create()
                .StartAt(date)
                .Build();

            return scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}
