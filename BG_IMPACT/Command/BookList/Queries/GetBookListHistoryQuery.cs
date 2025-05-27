using BG_IMPACT.Command.Order.Queries;
using BG_IMPACT.Extensions;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;

namespace BG_IMPACT.Command.BookList.Queries
{
    public class GetBookListHistoryQuery : IRequest<ResponseObject>
    {
        public Paging Paging { get; set; } = new();
        public class GetBookListHistoryQueryHandler : IRequestHandler<GetBookListHistoryQuery, ResponseObject>
        {
            public readonly IBookListRepository _bookListRepository;
            public readonly IHttpContextAccessor _httpContextAccessor;

            public GetBookListHistoryQueryHandler(IBookListRepository bookListRepository, IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;
                _bookListRepository = bookListRepository;
            }
            public async Task<ResponseObject> Handle(GetBookListHistoryQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? UserID = context?.GetName() ?? null;
                string? Role = context?.GetRole() ?? null;

                object param = new
                {
                    UserID,
                    Role,
                    request.Paging.PageNum,
                    request.Paging.PageSize
                };

                object param2 = new
                {
                    UserID,
                    Role
                };

                var result = await _bookListRepository.spBookListHistory(param);
                var list = ((IEnumerable<dynamic>)result).ToList();

                var pageData = await _bookListRepository.spBookListHistoryPageData(param2);
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
                            g.from,
                            g.to,
                            g.type,
                            g.total_price,
                            g.status
                        })
                        .Select(x => new
                        {
                            x.Key.id,
                            x.Key.customer_id,
                            x.Key.from,
                            x.Key.to,
                            x.Key.type,
                            x.Key.total_price,
                            x.Key.status,
                            items = x
                                .Where(i => i.book_list_id == x.Key.id)
                                .Select(t => new
                                {
                                    t.product_name,
                                    t.rent_price,
                                    t.rent_price_per_hour,
                                    t.condition,
                                    t.status
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
