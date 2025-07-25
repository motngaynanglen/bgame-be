using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Repositories.Interfaces
{
    public interface IBookTableRepository
    {
        Task<object?> spBookTableCreateByCustomer(object param);
    }
}
