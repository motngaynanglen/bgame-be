using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.BookList.Commands
{
    public class EndBookListCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid BookListId { get; set; }

        public class EndBookListCommandHandler : IRequestHandler<EndBookListCommand, ResponseObject>
        {
            private readonly IBookListRepository _bookListRepository;

            public EndBookListCommandHandler(IBookListRepository bookListRepository)
            {
                _bookListRepository = bookListRepository;
            }

            public async Task<ResponseObject> Handle(EndBookListCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.BookListId
                };

                var result = await _bookListRepository.spBookListEnd(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    if (count == 1) 
                    {
                        response.StatusCode = "404";
                        response.Message = "Không tìm thấy đơn thuê.";
                    }
                    else
                    {
                        response.StatusCode = "200";
                        response.Message = "Kết thúc dịch vụ thành công";
                    }
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Kết thúc dịch vụ thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
    }
}
