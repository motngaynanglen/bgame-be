using BG_IMPACT.Extensions;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Implementations;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;

namespace BG_IMPACT.Command.BookList.Queries
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

                    var result = await _bookListRepository.spBookListGet(param);
                    var list = ((IEnumerable<dynamic>)result).ToList();

                    if (list.Count > 0)
                    {
                        response.StatusCode = "200";
                        response.Data = list;
                        response.Message = string.Empty;
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
