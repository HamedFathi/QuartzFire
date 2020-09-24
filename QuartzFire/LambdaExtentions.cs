using Quartz;
using System;
using System.Threading.Tasks;

namespace Quartz.Lambda
{
    public static class LambdaExtentions
    {
        /// <summary>
        /// Schedule action with delay and repeat interval.
        /// </summary>
        /// <param name="action">Action to schedule</param>
        /// <param name="delay">Timespan delay</param>
        /// <param name="interval">Timespan execution interval</param>
        /// <returns></returns>
        public static Task<DateTimeOffset> ScheduleJob(this IScheduler scheduler, Action action, TimeSpan delay, TimeSpan interval)
        {
            var data = new JobDataMap{{"action", action}};
            var jobDetail = JobBuilder
                .Create<Job>()
                .UsingJobData(data)
                .Build();

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
        public static Task<DateTimeOffset> ScheduleJob(this IScheduler scheduler, Action action, int delay, int interval) =>
            ScheduleJob(scheduler, action, new TimeSpan(0, 0, 0, delay), new TimeSpan(0, 0, 0, interval));

        /// <summary>
        /// Schedule action with trigger builder.
        /// </summary>
        /// <param name="action">Action to schedule</param>
        /// <param name="triggerBuilder">Trigger builder</param>
        /// <returns></returns>
        public static Task<DateTimeOffset> ScheduleJob(this IScheduler scheduler, Action action, Func<TriggerBuilder, TriggerBuilder> triggerBuilder)
        {
            var data = new JobDataMap{{"action", action}};
            var job = JobBuilder
                .Create<Job>()
                .UsingJobData(data)
                .Build();

            var trigger = triggerBuilder(TriggerBuilder.Create()).Build();

            return scheduler.ScheduleJob(job, trigger);
        }
        
        private class Job : IJob
        {
            public Task Execute(IJobExecutionContext context)
            {
                return Task.Run(() => (context.JobDetail.JobDataMap["action"] as Action)?.Invoke());
            }
        }
    }
}