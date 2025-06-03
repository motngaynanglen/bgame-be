using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Repository.Repositories.Interfaces
{
    public interface ISupplyOrderRepository
    {
        Task<object> spSupplyOrderCreate(object param);
    }
}
