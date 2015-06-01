using System;
using System.Threading.Tasks;
using Nito.AsyncEx;
using Quartz;
using Topshelf.Logging;

namespace BoxUserMgmt.JobService
{
    internal class AssignEmployeeIdJob : IJob
    {
        private static readonly LogWriter Log = HostLogger.Get<AssignEmployeeIdJob>();

        public void Execute(IJobExecutionContext context)
        {
            AsyncContext.Run(() => ExecuteAsync(context));
        }

        public async Task ExecuteAsync(IJobExecutionContext context)
        {
            try
            {
                Log.Info("Process Queued Surveys Job Started...");

                await BoxUserMgmtClient.Instance.AssignEmployeeIdTask();

                Log.Info("Process Queued Surveys Job Completed Successfully...");
            }
            catch (Exception exception)
            {
                Log.Error(exception.ToString);
            }
        }
        
    }
}
