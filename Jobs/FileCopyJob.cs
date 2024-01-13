using Quartz;
using DesktopApplication.Repository;

namespace DesktopApplication.Jobs {
    internal class FileCopyJob : IJob {
        public async Task Execute(IJobExecutionContext context) {
            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string sourcePath = dataMap.GetString("SourcePath");
            string destinationPath = dataMap.GetString("DestinationPath");

            SyncObjectRepository.CopyFiles(sourcePath, destinationPath);

            Console.Out.WriteLineAsync($"Files copied from {sourcePath} to {destinationPath}");

        }
    }
}
