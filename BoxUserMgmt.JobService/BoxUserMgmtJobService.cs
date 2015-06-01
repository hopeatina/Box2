using Topshelf;
using Topshelf.Logging;

namespace BoxUserMgmt.JobService
{
    internal class BoxUserMgmtJobService : ServiceControl
    {
        private static readonly LogWriter Log = HostLogger.Get<BoxUserMgmtJobService>();

        public bool Start(HostControl hostControl)
        {
            Log.Info("Box User Mgmt Job Service Started...");

            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            Log.Info("Box User Mgmt Service Stopped...");

            return true;
        }
    }
}
