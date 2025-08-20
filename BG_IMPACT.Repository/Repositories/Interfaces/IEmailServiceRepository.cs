using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BG_IMPACT.Repository.Repositories.Implementations.EmailServiceRepository;

namespace BG_IMPACT.Repository.Repositories.Interfaces
{
    public interface IEmailServiceRepository
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);

        Task SendOrderSuccessfullyAsync(string email, string code, List<OrderItemModel> items);
    }
}
