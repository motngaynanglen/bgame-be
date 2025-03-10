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

        public class GetBookListByDateQueryHandler : IRequestHandler<GetBookListByDateQuery, ResponseObject>
        {
            private readonly IBookListRepository _bookListRepository;

            public GetBookListByDateQueryHandler(IBookListRepository bookListRepository)
            {
                _bookListRepository = bookListRepository;
            }
            public async Task<ResponseObject> Handle(GetBookListByDateQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
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

                return response;
            }
        }
    }
}
