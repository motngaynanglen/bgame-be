using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Store.Queries
{
    public class GetStoreByIdByAdminQuery : IRequest<ResponseObject>
    {
        public Guid Id { get; set; }

        public class GetStoreByIdByAdminQueryHandler : IRequestHandler<GetStoreByIdByAdminQuery, ResponseObject>
        {
            private readonly IStoreRepository _storeRepository;

            public GetStoreByIdByAdminQueryHandler(IStoreRepository storeRepository)
            {
                _storeRepository = storeRepository;
            }

            public async Task<ResponseObject> Handle(GetStoreByIdByAdminQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    StoreId = request.Id
                };

                var result = await _storeRepository.spStoreGetByIdByAdmin(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && dict.Count > 0)
                {
                    response.StatusCode = "200";
                    response.Data = dict;
                    response.Message = string.Empty;
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy cửa hàng.";
                }

                return response;
            }
        }

    }
}
