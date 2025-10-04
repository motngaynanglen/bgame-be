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
        Task<object> spEmailGetSupplierEmailByOrderId(object param);
        Task<object> spGetSupplyItemsByOrderId(object param);
        Task<object> spSupplyOrderUpdate(object param);
        Task<object> spSupplyOrderDeactive(object param);
    }
}
