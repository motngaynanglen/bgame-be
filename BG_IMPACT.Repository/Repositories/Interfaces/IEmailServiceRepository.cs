using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Repository.Repositories.Interfaces
{
    public interface IEmailServiceRepository
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
