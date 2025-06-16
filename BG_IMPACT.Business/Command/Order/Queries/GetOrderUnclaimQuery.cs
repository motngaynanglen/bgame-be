namespace BG_IMPACT.Business.Command.Order.Queries
{
    public class GetOrderGetUnclaimQuery : IRequest<ResponseObject>
    {
        public Paging Paging { get; set; } = new();
        public class GetOrderGetUnclaimQueryHandler : IRequestHandler<GetOrderGetUnclaimQuery, ResponseObject>
        {
            public readonly IOrderRepository _orderRepository;
            public readonly IHttpContextAccessor _httpContextAccessor;

            public GetOrderGetUnclaimQueryHandler(IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;
                _orderRepository = orderRepository;
            }
            public async Task<ResponseObject> Handle(GetOrderGetUnclaimQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                object param = new
                {
                    request.Paging.PageNum,
                    request.Paging.PageSize
                };

                object param2 = new
                {
                };

                var result = await _orderRepository.spOrderGetUnclaim(param);
                var list = ((IEnumerable<dynamic>)result).ToList();

                var pageData = await _orderRepository.spOrderGetUnclaimPageData(param2);
                var dict = pageData as IDictionary<string, object>;
                long count = 0;

                if (dict != null && Int64.TryParse(dict["TotalRows"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["TotalRows"].ToString(), out count);
                }

                if (list.Count > 0)
                {
                    long pageCount = count / request.Paging.PageSize;

                    object data = list
                        .GroupBy(g => new
                        {
                            g.id,
                            g.customer_id,
                            g.full_name,
                            g.phone_number,
                            g.address,
                            g.total_price,
                            g.status
                        })
                        .Select(x => new
                        {
                            x.Key.id,
                            x.Key.customer_id,
                            x.Key.full_name,
                            x.Key.phone_number,
                            x.Key.address,
                            x.Key.total_price,
                            items = x
                                .Where(i => i.order_id == x.Key.id)
                                .Select(t => new
                                {
                                    t.product_template_id,
                                    t.product_name,
                                    //t.semi_product_id,
                                    //t.semi_product_name,
                                    t.current_price
                                }).ToList()
                        })
                        .ToList();

                    response.StatusCode = "200";
                    response.Data = data;
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
            }
        }
    }
}
