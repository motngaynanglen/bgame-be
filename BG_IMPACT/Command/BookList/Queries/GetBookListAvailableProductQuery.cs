using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.BookList.Queries
{
    public class GetBookListAvailableProductQuery : IRequest<ResponseObject>
    {
        [Required]
        public Guid StoreID { get; set; }
        [Required]
        public DateTimeOffset Date { get; set; }

        public class GetBookListAvailableProductQueryHandler : IRequestHandler<GetBookListAvailableProductQuery, ResponseObject>
        {
            public readonly IBookListRepository _bookListRepository;

            public GetBookListAvailableProductQueryHandler(IBookListRepository bookListRepository)
            {
                _bookListRepository = bookListRepository;
            }

            public async Task<ResponseObject> Handle(GetBookListAvailableProductQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.StoreID,
                    request.Date
                };


                var result = await _bookListRepository.spBookListGetAvailableProduct(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && dict.Count > 0)
                {
                    response.StatusCode = "200";
                    response.Data = dict;
                    response.Message = string.Empty;
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy sản phẩm.";
                }

                return response;
            }
        }
    }
}
