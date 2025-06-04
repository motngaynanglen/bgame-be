using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Repository.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<object> spCategoryCreate(object param);
        Task<object> spCategoryUpdate(object param);
        Task<object> spCategoryGetList();
        Task<object> spCategoryGetListByAdmin();
        Task<object> spCategoryDeactive(object param);
        
    }
}
