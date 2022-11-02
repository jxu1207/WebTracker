using Quartz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using WebTracker.Model;
using WebTracker.ModelAgent;

namespace WebTracker.Job
{
    internal class JobSchedulerJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            var historyAgent = new JobHistoryAgent();
            historyAgent.CreateHistory("JobSchedulerJob", "Running");

            var jobAgent = new JobAgent();
            List<Model.Job> jobs = jobAgent.GetActiveJob();

            foreach (var job in jobs) 
            {
                var triggerId = $"{job.JobName}Trigger";

                var newTrigger = TriggerBuilder.Create().WithIdentity(triggerId)
                    .WithDescription($"{job.JobName} Trigger")
                    .WithCronSchedule(job.CronExpression);

                context.Scheduler.ScheduleJob(JobBuilder.Create(typeof(WebTrackerJob))
                        .WithIdentity(job.Id.ToString(), "JobScheduler")
                        .WithDescription(job.JobName)
                        .Build(), newTrigger.Build());
            }

            return Task.CompletedTask;
        }
    }
}
