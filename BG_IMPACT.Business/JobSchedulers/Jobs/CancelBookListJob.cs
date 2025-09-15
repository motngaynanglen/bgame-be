using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.JobSchedulers.Jobs
{
    public class CancelBookListJob : IJob
    {
        private readonly IBookListRepository _bookListRepository;
        private readonly ILogger _logger;

        public CancelBookListJob(ILogger logger, IBookListRepository bookListRepository)
        {
            _logger = logger;
            _bookListRepository = bookListRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var id = context.JobDetail.JobDataMap.GetString("id");

            _logger.LogInformation("Job has been started.");

            var param = new
            {
                BookListId = id,
                IsAdmin = true
            };

            if (string.IsNullOrEmpty(id))
            {
                _logger.LogInformation("No RequestId provided.");
                return;
            }

            var result = await _bookListRepository.spBookListCancel(param);
            var dict = result as IDictionary<string, object>;

            if (dict != null && Int64.TryParse(dict["Status"].ToString(), out long count) == true)
            {
                if (count == 0) _logger.LogInformation($"BookList {id} cancelled SUCCESSFULLY at {DateTime.Now}.");
                else _logger.LogInformation($"BookList {id} cancelled FAILED at {DateTime.Now}.");

            }
            else
            {
                _logger.LogInformation($"Job run failed");
            }
        }
    }
}
