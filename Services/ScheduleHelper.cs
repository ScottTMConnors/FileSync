using DesktopApplication.Jobs;
using DesktopApplication.Objects;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopApplication.Services {
    internal class ScheduleHelper {
        private static IScheduler scheduler;

        internal static async Task InitiailzeScheduler() {
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            scheduler = await schedulerFactory.GetScheduler();
            await scheduler.Start();

        }

        public static async Task ScheduleFileCopyTask(SyncObject task) {
            IJobDetail job = JobBuilder.Create<FileCopyJob>()
            .WithIdentity($"Job{task.Id}", "fileCopyGroup")
            .UsingJobData("SourcePath", task.sourcePath)
            .UsingJobData("DestinationPath", task.destinationPath)
            .Build();

            // Create a trigger for the job
            ITrigger trigger;
            if (task.IsRecurring && task.RecurrenceInterval.HasValue) {
                trigger = TriggerBuilder.Create()
                    .WithIdentity($"Trigger{task.Id}", "fileCopyGroup")
                    .StartAt(task.ScheduledTime.Value)
                    .WithSimpleSchedule(x => x
                        .WithInterval(task.RecurrenceInterval.Value)
                        .RepeatForever())
                    .Build();
            } else {
                trigger = TriggerBuilder.Create()
                    .WithIdentity($"Trigger{task.Id}", "fileCopyGroup")
                    .StartAt(task.ScheduledTime.Value)
                    .Build();
            }

            // Tell Quartz to schedule the job using our trigger
            await scheduler.ScheduleJob(job, trigger);
        }



    }
}
