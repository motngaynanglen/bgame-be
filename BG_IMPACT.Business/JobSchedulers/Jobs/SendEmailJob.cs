using Azure.Core;
using BG_IMPACT.Repository.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quartz;
using System.Reflection.Metadata;
using static BG_IMPACT.Business.Command.Order.Commands.CreateOrderByCustomerCommand;

namespace BG_IMPACT.Business.JobSchedulers.Jobs
{
    public class SendEmailJob : IJob
    {
        private readonly ILogger<SendEmailJob> _logger;
        private readonly IEmailServiceRepository _emailServiceRepository;

        public SendEmailJob(ILogger<SendEmailJob> logger, IEmailServiceRepository emailServiceRepository)
        {
            _logger = logger;
            _emailServiceRepository = emailServiceRepository;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var requestId = context.JobDetail.Key.Name;
            var jobData = context.JobDetail.JobDataMap;

            _logger.LogInformation("Job has been started.");

            if (string.IsNullOrEmpty(requestId))
            {
                _logger.LogInformation("No RequestId provided.");
                return;
            }

            var rawJson = context.JobDetail.JobDataMap.GetString("payload");

            if (rawJson == null)
            {
                _logger.LogInformation("Payload is null, sending email failed.");
                return;
            }

            var order = JsonConvert.DeserializeObject<OrderGroupModel>(rawJson);
            var items = order.orders.SelectMany(x => x.order_items).ToList();

            await _emailServiceRepository.SendOrderSuccessfullyAsync(order.email, order.order_code, items);

            _logger.LogInformation($"Send email request: {requestId} finished at {DateTime.Now}.");
        }
    }
}
