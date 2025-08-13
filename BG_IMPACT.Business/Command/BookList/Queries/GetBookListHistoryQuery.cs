namespace BG_IMPACT.Business.Command.BookList.Queries
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

                var result = await _bookListRepository.spBookListGetPaged(param);
                var list = ((IEnumerable<dynamic>)result.bookLists).ToList();
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
            }
        }
    }
}
