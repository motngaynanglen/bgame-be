using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.SupplyOrder.Queries
{
    public class GetSupplyOrderListQuery : IRequest<ResponseObject>
    {
        public Paging Paging { get; set; } = new();

        public class GetSupplyOrderListQueryHandler : IRequestHandler<GetSupplyOrderListQuery, ResponseObject>
        {
            private readonly ISupplyOrderRepository _supplyOrderRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;
            public GetSupplyOrderListQueryHandler(ISupplyOrderRepository supplyOrderRepository, IHttpContextAccessor httpContextAccessor)
            {
                _supplyOrderRepository = supplyOrderRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(GetSupplyOrderListQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string ManagerID = context.GetName();

                object param = new
                {
                    ManagerID,
                    request.Paging.PageNum,
                    request.Paging.PageSize
                };


                var result = await _supplyOrderRepository.spSupplyOrderGetList(param);
                var list = ((IEnumerable<dynamic>)result.supplyOrders).ToList();
                long count = result.totalCount;

                if (list.Count > 0)
                {
                    long pageCount = count / request.Paging.PageSize;

                    response.StatusCode = "200";
                    response.Data = list;
                    response.Message = string.Empty;
                    response.Paging = new PagingModel
                    {
                        PageNum = request.Paging.PageNum,
                        PageSize = request.Paging.PageSize,
                        PageCount = count % request.Paging.PageSize == 0 ? pageCount : pageCount + 1
                    };
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy đơn nhập hàng nào.";
                }
                return response;
            }
        }
    }
}
