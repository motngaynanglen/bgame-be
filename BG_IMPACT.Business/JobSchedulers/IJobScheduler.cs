using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Jobs
{
    public interface IJobScheduler
    {
        Task<string> ScheduleOnDemandJobAsync(string requestId, Type jobType, TimeSpan delay, string? id, object? payload);
        Task ScheduleDailyJobAsync();
        Task CancelJobAsync(string requestId, string group = "OnDemandJobs");
    }
}
