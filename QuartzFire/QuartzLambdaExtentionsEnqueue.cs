using System;
using System.Threading.Tasks;

namespace Quartz
{
    public static partial class QuartzLambdaExtentions
    {
        public static Task<DateTimeOffset> Enqueue(this IScheduler scheduler, Action action, bool disallowConcurrentJob = false)
        {
            IJobDetail jobDetail;
            if (disallowConcurrentJob)
            {
                var data = new JobDataMap { { "disallowConcurrentAction", action } };
                jobDetail = JobBuilder
                    .Create<DisallowConcurrentJob>()
                    .UsingJobData(data)
                    .Build();
            }
            else
            {
                var data = new JobDataMap { { "action", action } };
                jobDetail = JobBuilder
                    .Create<Job>()
                    .UsingJobData(data)
                    .Build();
            }

            var trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule()
                .Build();

            return scheduler.ScheduleJob(jobDetail, trigger);
        }

        public static Task<DateTimeOffset> Enqueue<T>(this IScheduler scheduler, Action<T> action, bool disallowConcurrentJob = false)
            where T : new()
        {
            IJobDetail jobDetail;
            if (disallowConcurrentJob)
            {
                var data = new JobDataMap { { "disallowConcurrentActionT", action.Convert() }, { "DT", new T() } };
                jobDetail = JobBuilder
                    .Create<DisallowConcurrentJobT>()
                    .UsingJobData(data)
                    .Build();
            }
            else
            {
                var data = new JobDataMap { { "actionT", action.Convert() }, { "T", new T() } };
                jobDetail = JobBuilder
                    .Create<JobT>()
                    .UsingJobData(data)
                    .Build();
            }

            var trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule()
                .Build();

            return scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}
