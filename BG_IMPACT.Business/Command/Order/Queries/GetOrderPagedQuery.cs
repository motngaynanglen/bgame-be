using System.Dynamic;

namespace BG_IMPACT.Business.Command.Order.Queries
{
    public class GetOrderPagedQuery : IRequest<ResponseObject>
    {
        public string? Keyword { get; set; }
        public List<string>? Status { get; set; }
        public DateTimeOffset? CreatedFrom { get; set; }
        public DateTimeOffset? CreatedTo { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
        public Paging Paging { get; set; } = new();
        public class GetOrderPagedQueryHandler : IRequestHandler<GetOrderPagedQuery, ResponseObject>
        {
            public readonly IOrderRepository _orderRepository;
            public readonly IOrderItemRepository _itemRepository;
            public readonly IHttpContextAccessor _httpContextAccessor;

            public GetOrderPagedQueryHandler(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;
                _orderRepository = orderRepository;
                _itemRepository = orderItemRepository;
            }
            public async Task<ResponseObject> Handle(GetOrderPagedQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? UserID = context?.GetName() ?? null;
                string? Role = context?.GetRole() ?? null;

                object param = new
                {
                    request.Keyword,
                    Status = request.Status != null ? string.Join(",", request.Status) : null,
                    request.CreatedFrom,
                    request.CreatedTo,
                    request.SortColumn,
                    request.SortDirection,

                    PageNum = request.Paging.PageNum,
                    PageSize = request.Paging.PageSize,
                    ExcuteUserRole = Role,
                    ExcuteUserID = UserID,
                };


                var result = await _orderRepository.spOrderGetPaged(param);
                var list = ((IEnumerable<dynamic>)result.orderGroups).ToList();
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
                    response.Message = "Không tìm thấy đơn hàng nào.";
                }

                return response;
                //var ordersRaw = ((IEnumerable<dynamic>)result).ToList();
                //if (!ordersRaw.Any())
                //{
                //    return new ResponseObject
                //    {
                //        StatusCode = "200",
                //        Message = "Danh sách trống",
                //        Data = null
                //    };
                //}
                //int totalCount = ordersRaw[0].TotalCount;
                //var ordersCleaned = ordersRaw.Select(o =>
                //{
                //    var dict = (IDictionary<string, object>)o;
                //    var orderDict = new ExpandoObject() as IDictionary<string, object>;

                //    foreach (var kv in dict)
                //    {
                //        if (kv.Key != "TotalCount")
                //            orderDict[kv.Key] = kv.Value;
                //    }

                //    return orderDict;
                //}).ToList();

                //var orderIds = ordersCleaned.Select(o => (Guid)o["id"]).ToList();
                //object OrderIDListParam = new
                //{
                //    OrderIDList = string.Join(",", orderIds),
                //};
                //var orderItemsRaw = await _itemRepository.spOrderItemGetItemsByOrderIds(OrderIDListParam);
                //var orderItems = ((IEnumerable<dynamic>)orderItemsRaw).ToList();

                //foreach (var order in ordersCleaned)
                //{
                //    var orderId = (Guid)order["id"];
                //    order["Items"] = orderItems.Where(i => i.order_id == orderId).ToList();
                //}
                //var pageCount = (int)Math.Ceiling((double)totalCount / request.Paging.PageSize);

                //return new ResponseObject
                //{
                //    StatusCode = "200",
                //    Message = string.Empty,
                //    Data = ordersCleaned,
                //    Paging = new PagingModel
                //    {
                //        PageNum = request.Paging.PageNum,
                //        PageSize = request.Paging.PageSize,
                //        PageCount = pageCount,
                //    }
                //};
            }
        }
    }
}
