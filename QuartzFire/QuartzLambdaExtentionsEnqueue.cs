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
                var data = new JobDataMap { { "DisallowConcurrentJobTypeAction", action.Convert() }, { "DisallowConcurrentJobType", new T() } };
                jobDetail = JobBuilder
                    .Create<DisallowConcurrentJobType>()
                    .UsingJobData(data)
                    .Build();
            }
            else
            {
                var data = new JobDataMap { { "JobTypeAction", action.Convert() }, { "JobType", new T() } };
                jobDetail = JobBuilder
                    .Create<JobType>()
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
