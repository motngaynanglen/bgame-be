using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Repository.Repositories.Interfaces
{
    public interface INewsRepository
    {
        Task<object> spNewsCreate(object param);
        Task<object> spNewsUpdate(object param);
        Task<object> spNewsGetList(object param);
        Task<object> spNewsDeactive(object param);
    }
}
