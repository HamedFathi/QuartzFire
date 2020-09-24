using System;
using System.Threading.Tasks;

namespace Quartz
{
    public static partial class QuartzLambdaExtentions
    {
        internal class Job : IJob
        {
            public Task Execute(IJobExecutionContext context)
            {
                return Task.Run(() => (context.JobDetail.JobDataMap["JobAction"] as Action)?.Invoke());
            }
        }

        internal class JobType : IJob
        {
            public Task Execute(IJobExecutionContext context)
            {
                var obj = context.JobDetail.JobDataMap["JobType"];
                return Task.Run(() => (context.JobDetail.JobDataMap["JobTypeAction"] as Action<object>)?.Invoke(obj));
            }
        }

        [DisallowConcurrentExecution]
        internal class DisallowConcurrentJob : IJob
        {
            public Task Execute(IJobExecutionContext context)
            {
                return Task.Run(() => (context.JobDetail.JobDataMap["DisallowConcurrentJobAction"] as Action)?.Invoke());
            }
        }

        [DisallowConcurrentExecution]
        internal class DisallowConcurrentJobType : IJob
        {
            public Task Execute(IJobExecutionContext context)
            {
                var obj = context.JobDetail.JobDataMap["DisallowConcurrentJobType"];
                return Task.Run(() => (context.JobDetail.JobDataMap["DisallowConcurrentJobTypeAction"] as Action<object>)?.Invoke(obj));
            }
        }
    }
}