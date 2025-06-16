using BG_IMPACT.DTO.Models.PagingModels;

namespace BG_IMPACT.Business.Command.BookList.Queries
{
    public class GetBookListByDateQuery : IRequest<ResponseObject>
    {
        public DateTimeOffset From { get; set; }
        public DateTimeOffset To { get; set; }
        public Paging Paging { get; set; } = new Paging();

        public class GetBookListByDateQueryHandler : IRequestHandler<GetBookListByDateQuery, ResponseObject>
        {
            private readonly IBookListRepository _bookListRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public GetBookListByDateQueryHandler(IBookListRepository bookListRepository, IHttpContextAccessor httpContextAccessor)
            {
                _bookListRepository = bookListRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(GetBookListByDateQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;

                string? UserID = context?.GetName() ?? null;
                string? Role = context?.GetRole() ?? null;

                if (Role == null || Guid.TryParse(UserID, out _) == false)
                {
                    response.StatusCode = "404";
                    response.Message = "Token trả về không đúng.";
                }
                else
                {
                    object param = new
                    {
                        request.From,
                        request.To,
                        UserID,
                        Role,
                        request.Paging.PageNum,
                        request.Paging.PageSize
                    };

                    object param2 = new
                    {
                        request.From,
                        request.To,
                        UserID,
                        Role
                    };

                    var result = await _bookListRepository.spBookListGet(param);
                    var list = ((IEnumerable<dynamic>)result).ToList();

                    var pageData = await _bookListRepository.spBookListGetPageData(param2);
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
                        response.Message = "Không tìm thấy đơn thuê nào.";
                    }
                }

                return response;
            }
        }
    }
}
