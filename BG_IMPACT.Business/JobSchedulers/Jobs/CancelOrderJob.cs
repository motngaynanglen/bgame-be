using Azure;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.JobSchedulers.Jobs
{
    public class CancelOrderJob : IJob
    {
        private readonly ILogger<CancelOrderJob> _logger;
        private readonly IOrderRepository _orderRepository;

        public CancelOrderJob(IOrderRepository orderRepository, ILogger<CancelOrderJob> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var id = context.JobDetail.JobDataMap.GetString("id");

            _logger.LogInformation("Job has been started.");

            var param = new
            {
                OrderId = id,
                IsAdmin = true
            };

            if (string.IsNullOrEmpty(id))
            {
                _logger.LogInformation("No RequestId provided.");
                return;
            }

            var result = await _orderRepository.spOrderCancel(param);
            var dict = result as IDictionary<string, object>;

            if (dict != null && Int64.TryParse(dict["Status"].ToString(), out long count) == true)
            {
                if (count == 0) _logger.LogInformation($"Order {id} cancelled SUCCESSFULLY at {DateTime.Now}.");
                else _logger.LogInformation($"Order {id} cancelled FAILED at {DateTime.Now}.");

            }
            else
            {
                _logger.LogInformation($"Job run failed");
            }
        }
    }
}
