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
        public Guid StoreId { get; set; }
        public class GetStoreTableListByStoreIDQueryHandler : IRequestHandler<GetStoreTableListByStoreIDQuery, ResponseObject>
        {
            private readonly IStoreTableRepository _storeTableRepository;

            public GetStoreTableListByStoreIDQueryHandler(IStoreTableRepository storeTableRepository)
            {
                _storeTableRepository = storeTableRepository;
            }
            public async Task<ResponseObject> Handle(GetStoreTableListByStoreIDQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.StoreId
                };

                var result = await _storeTableRepository.spStoreTableGetListByStoreID(param);

                if (result == null)
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy.";
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
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy cửa hàng nào.";
                }

                return response;
            }
        }
    }
}
