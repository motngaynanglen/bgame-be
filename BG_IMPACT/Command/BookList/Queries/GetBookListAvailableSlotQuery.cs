using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using CloudinaryDotNet.Actions;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.BookList.Queries
{
    public class GetBookListAvailableSlotQuery : IRequest<ResponseObject>
    {
        [Required]
        public Guid StoreID { get; set; }
        [Required]
        public DateTimeOffset Date { get; set; }

        public class GetBookListAvailableSlotQueryHandler : IRequestHandler<GetBookListAvailableSlotQuery, ResponseObject>
        {
            public readonly IBookListRepository _bookListRepository;
           
            public GetBookListAvailableSlotQueryHandler(IBookListRepository bookListRepository)
            {
                _bookListRepository = bookListRepository;
            }

            public async Task<ResponseObject> Handle(GetBookListAvailableSlotQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.StoreID,
                    request.Date
                };


                var result = await _bookListRepository.spBookListGetAvailableSlot(param);
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
