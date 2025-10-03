namespace BG_IMPACT.Business.Command.BookList.Queries
{
    public class GetBookListPagedQuery : IRequest<ResponseObject>
    {
        public string? Keyword { get; set; }
        public List<string>? Status { get; set; }
        public DateTimeOffset? CreatedFrom { get; set; }
        public DateTimeOffset? CreatedTo { get; set; }
        public string? SortColumn { get; set; }
        public string? SortDirection { get; set; }
        public Paging Paging { get; set; } = new();
        public class GetBookListPagedQueryHandler : IRequestHandler<GetBookListPagedQuery, ResponseObject>
        {
            public readonly IBookListRepository _bookListRepository;
            public readonly IHttpContextAccessor _httpContextAccessor;

            public GetBookListPagedQueryHandler(IBookListRepository bookListRepository, IHttpContextAccessor httpContextAccessor)
            {
                _httpContextAccessor = httpContextAccessor;
                _bookListRepository = bookListRepository;
            }
            public async Task<ResponseObject> Handle(GetBookListPagedQuery request, CancellationToken cancellationToken)
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
                    response.Message = "Không tìm thấy đơn thuê nào.";
                }

                return response;
            }
        }
    }
}
