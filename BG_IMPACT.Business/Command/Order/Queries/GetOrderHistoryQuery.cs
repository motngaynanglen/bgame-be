﻿using BG_IMPACT.DTO.Models.PagingModels;

namespace BG_IMPACT.Business.Command.Order.Queries
{
    public class GetOrderHistoryQuery : IRequest<ResponseObject>
    {
        public Filter? Filter { get; set; }
        public Paging Paging { get; set; } = new();
        public class GetOrderHistoryQueryHandler : IRequestHandler<GetOrderHistoryQuery, ResponseObject>
        {
            public readonly IOrderRepository _orderRepository;
            public readonly IHttpContextAccessor _httpContextAccessor;

            public GetOrderHistoryQueryHandler(IOrderRepository orderRepository, IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;
                _orderRepository = orderRepository;
            }
            public async Task<ResponseObject> Handle(GetOrderHistoryQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? UserID = context?.GetName() ?? null;
                string? Role = context?.GetRole() ?? null;

                object param = new
                {
                    UserID,
                    Role,
                    request.Filter?.Status,
                    request.Paging.PageNum,
                    request.Paging.PageSize
                };

                object param2 = new
                {
                    UserID,
                    Role
                };

                var result = await _orderRepository.spOrderHistory(param);
                var list = ((IEnumerable<dynamic>)result).ToList();

                var pageData = await _orderRepository.spOrderHistoryPageData(param2);
                var dict = pageData as IDictionary<string, object>;
                long count = 0;

                if (dict != null && Int64.TryParse(dict["TotalRows"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["TotalRows"].ToString(), out count);
                }

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
            }
        }
    }
}
