using System;
using System.Threading.Tasks;

namespace Quartz
{
    public static partial class QuartzLambdaExtentions
    {       
        /// <summary>
        /// Schedule action with delay and repeat interval.
        /// </summary>
        /// <param name="action">Action to schedule</param>
        /// <param name="delay">Timespan delay</param>
        /// <param name="interval">Timespan execution interval</param>
        /// <returns></returns>
        public static Task<DateTimeOffset> ScheduleJob(this IScheduler scheduler, Action action, TimeSpan delay, TimeSpan interval, bool disallowConcurrentJob = false)
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
                .StartAt(DateTimeOffset.UtcNow.Add(delay))
                .WithSimpleSchedule(s => s.WithInterval(interval).RepeatForever())
                .Build();

            return scheduler.ScheduleJob(jobDetail, trigger);
        }

        /// <summary>
        /// Schedule action with delay and repeat interval.
        /// </summary>
        /// <param name="action">Action to schedule</param>
        /// <param name="delay">Dealy in secconds</param>
        /// <param name="interval">Execution interval in seconds</param>
        /// <returns></returns>
        public static Task<DateTimeOffset> ScheduleJob(this IScheduler scheduler, Action action, int delay, int interval, bool disallowConcurrentJob = false) =>
            ScheduleJob(scheduler, action, new TimeSpan(0, 0, 0, delay), new TimeSpan(0, 0, 0, interval), disallowConcurrentJob);

        /// <summary>
        /// Schedule action with trigger builder.
        /// </summary>
        /// <param name="action">Action to schedule</param>
        /// <param name="triggerBuilder">Trigger builder</param>
        /// <returns></returns>
        public static Task<DateTimeOffset> ScheduleJob(this IScheduler scheduler, Action action, Func<TriggerBuilder, TriggerBuilder> triggerBuilder, bool disallowConcurrentJob = false)
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

            var trigger = triggerBuilder(TriggerBuilder.Create()).Build();

            return scheduler.ScheduleJob(jobDetail, trigger);
        }
    }
}