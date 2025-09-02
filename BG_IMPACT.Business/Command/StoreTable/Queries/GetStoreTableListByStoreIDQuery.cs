using BG_IMPACT.Business.Command.Store.Queries;
using BG_IMPACT.Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.StoreTable.Queries
{
    public class GetStoreTableListByStoreIDQuery : IRequest<ResponseObject>
    {
        [Required]
        public Guid? StoreId { get; set; } = null;
        public class GetStoreTableListByStoreIDQueryHandler : IRequestHandler<GetStoreTableListByStoreIDQuery, ResponseObject>
        {
            private readonly IStoreTableRepository _storeTableRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetStoreTableListByStoreIDQueryHandler(IStoreTableRepository storeTableRepository, IHttpContextAccessor httpContextAccessor)
            {
                _storeTableRepository = storeTableRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(GetStoreTableListByStoreIDQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;

                object param = new
                {
                    request.StoreId,
                };

                _ = Guid.TryParse(context.GetName(), out Guid cusId);

                if (context != null && context.GetRole() == "MANAGER")
                {
                    _ = Guid.TryParse(context.GetName(), out Guid userId);
                    param = new
                    {
                        request.StoreId,
                        cusId
                    };
                }
               
                var result = await _storeTableRepository.spStoreTableGetListByStoreID(param);

                if (result == null)
                {
                    response.StatusCode = "404";
                    response.Message = "Hệ thống bảo trì hoặc đang nâng cấp.";
                    return response;
                }
                var list = ((IEnumerable<dynamic>)result).ToList();

                if (list.Count > 0)
                {
                    response.StatusCode = "200";
                    response.Data = list;
                    response.Message = string.Empty;
                }
                else
                {
                    response.StatusCode = "200";
                    response.Message = "Cửa hàng chưa thêm bàn";
                }

                return response;
            }
        }
    }
}
