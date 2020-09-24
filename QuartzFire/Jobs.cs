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
                return Task.Run(() => (context.JobDetail.JobDataMap["action"] as Action)?.Invoke());
            }
        }

        internal class JobT : IJob
        {
            public Task Execute(IJobExecutionContext context)
            {
                var obj = context.JobDetail.JobDataMap["T"];
                return Task.Run(() => (context.JobDetail.JobDataMap["actionT"] as Action<object>)?.Invoke(obj));
            }
        }

        [DisallowConcurrentExecution]
        internal class DisallowConcurrentJob : IJob
        {
            public Task Execute(IJobExecutionContext context)
            {
                return Task.Run(() => (context.JobDetail.JobDataMap["disallowConcurrentAction"] as Action)?.Invoke());
            }
        }

        [DisallowConcurrentExecution]
        internal class DisallowConcurrentJobT : IJob
        {
            public Task Execute(IJobExecutionContext context)
            {
                var obj = context.JobDetail.JobDataMap["DT"];
                return Task.Run(() => (context.JobDetail.JobDataMap["disallowConcurrentActionT"] as Action<object>)?.Invoke(obj));
            }
        }
    }
}