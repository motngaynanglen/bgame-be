using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.SupplyOrder.Queries
{
    public class GetSupplyOrderByIdQuery : IRequest<ResponseObject>
    {
        public Guid SupplyOrderId { get; set; }

        public class GetSupplyOrderByIdQueryHandler : IRequestHandler<GetSupplyOrderByIdQuery, ResponseObject>
        {
            private readonly ISupplyOrderRepository _supplyOrderRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;
            public GetSupplyOrderByIdQueryHandler(ISupplyOrderRepository supplyOrderRepository, IHttpContextAccessor httpContextAccessor)
            {
                _supplyOrderRepository = supplyOrderRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(GetSupplyOrderByIdQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string ManagerID = context.GetName();

                object param = new
                {
                    request.SupplyOrderId,
                    ManagerID
                };

                var result = await _supplyOrderRepository.spSupplyOrderGetById(param);
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
                    response.Message = "Không tìm thấy đơn nhập hàng.";
                }

                return response;
            }
        }
    }
}
