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

        internal class JobWithType : IJob
        {
            public Task Execute(IJobExecutionContext context)
            {
                var obj = context.JobDetail.JobDataMap["JobWithType"];
                return Task.Run(() => (context.JobDetail.JobDataMap["JobWithTypeAction"] as Action<object>)?.Invoke(obj));
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
        internal class DisallowConcurrentJobWithType : IJob
        {
            public Task Execute(IJobExecutionContext context)
            {
                var obj = context.JobDetail.JobDataMap["DisallowConcurrentJobWithType"];
                return Task.Run(() => (context.JobDetail.JobDataMap["DisallowConcurrentJobWithTypeAction"] as Action<object>)?.Invoke(obj));
            }
        }
    }
}