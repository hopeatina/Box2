using Quartz;
using Topshelf;
using Topshelf.Ninject;
using Topshelf.Quartz;
using Topshelf.Quartz.Ninject;

namespace BoxUserMgmt.JobService
{
    public class Program
    {
        public static void Main()
        {
            HostFactory.Run(hostConfig =>
            {
                hostConfig.StartAutomatically();

                hostConfig.EnableServiceRecovery(recoveryConfig =>
                {
                    recoveryConfig.OnCrashOnly();
                    recoveryConfig.RestartService(1);
                });

                hostConfig.UseNinject();

                hostConfig.DependsOnEventLog();

                hostConfig.UseNLog();

                hostConfig.Service<BoxUserMgmtJobService>(serviceConfig =>
                {
                    serviceConfig.ConstructUsingNinject();

                    serviceConfig.WhenStarted((service, control) => service.Start(control));

                    serviceConfig.WhenStopped((service, control) => service.Stop(control));

                    serviceConfig.UseQuartzNinject();

                    serviceConfig.ScheduleQuartzJob(q =>
                        q.WithJob(() =>
                            JobBuilder.Create<AssignEmployeeIdJob>().Build())
                            .AddTrigger(() =>
                                TriggerBuilder.Create()
                                    .WithSimpleSchedule(builder => builder.WithIntervalInMinutes(1).RepeatForever())
                                    .Build()));
                });
            });
        }
    }
}
