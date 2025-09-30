using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Repository.Repositories.Interfaces
{
    public interface IProductTemplateRepository
    {
        
        Task<object?> spProductTemplateCreate(object param);
        Task<object?> spProductTemplateGetListByStoreID(object param);
        Task<object?> spProductTemplateGetListByGroupRefID(object param);
        Task<object?> spProductTemplateGetRentalsByStoreID(object param);
    }
}
