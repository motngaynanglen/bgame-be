using BG_IMPACT.Business.Jobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quartz;

namespace BG_IMPACT.Infrastructure.Jobs
{
    public class JobScheduler : IJobScheduler
    {
        private readonly ILogger<JobScheduler> _logger;
        private readonly ISchedulerFactory _schedulerFactory;

        public JobScheduler(ISchedulerFactory schedulerFactory, ILogger<JobScheduler> logger)
        {
            _schedulerFactory = schedulerFactory;
            _logger = logger;
        }

        public async Task<string> ScheduleOnDemandJobAsync(string requestId, Type jobType, TimeSpan delay, string? id, object? payload)
        {
            var scheduler = await _schedulerFactory.GetScheduler();

            var jobBuilder = JobBuilder.Create(jobType)
                .WithIdentity(requestId, "OnDemandJobs");

            if (id != null)
            {
                jobBuilder.UsingJobData("id", id);
            }

            if (payload != null)
            {
                var payloadJson = JsonConvert.SerializeObject(payload);

                jobBuilder = jobBuilder.UsingJobData("payload", payloadJson);
            }

            var job = jobBuilder.Build();

            var trigger = TriggerBuilder.Create()
                .StartAt(DateTimeOffset.Now.Add(delay))
                .Build();

            _logger.LogInformation(requestId + " has been created and execute after " + delay);

            await scheduler.ScheduleJob(job, trigger);
            return requestId;
        }

        //public async Task ScheduleDailyJobAsync(string jobId, Type jobType, string cronExpression)
        //{
        //    var scheduler = await _schedulerFactory.GetScheduler();

        //    var job = JobBuilder.Create(jobType)
        //        .WithIdentity(jobId, "DailyJobs")
        //        .Build();

        //    var trigger = TriggerBuilder.Create()
        //        .WithCronSchedule(cronExpression) // e.g., "0 0 0 * * ?" for midnight
        //        .Build();

        //    await scheduler.ScheduleJob(job, trigger);
        //}

        public async Task CancelJobAsync(string requestId, string group = "OnDemandJobs")
        {
            var scheduler = await _schedulerFactory.GetScheduler();
            await scheduler.DeleteJob(new JobKey(requestId, group));
        }

        public Task ScheduleDailyJobAsync()
        {
            throw new NotImplementedException();
        }
    }
}
