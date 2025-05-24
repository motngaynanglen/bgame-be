using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BG_IMPACT.Command.BookList.Queries
{
    public class GetBookListByIdQuery : IRequest<ResponseObject>
    {
        public Guid BookListID { get; set; }
        public class GetBookListByIdQueryHandler : IRequestHandler<GetBookListByIdQuery, ResponseObject>
        {
            private readonly IBookListRepository _BookListRepository;

            public GetBookListByIdQueryHandler(IBookListRepository BookListRepository)
            {
                _BookListRepository = BookListRepository;
            }
            public async Task<ResponseObject> Handle(GetBookListByIdQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.BookListID,
                };

                var result = await _BookListRepository.spBookListGetById(param);
                var rawData = result as IDictionary<string, object>;
                var BookList = JsonConvert.DeserializeObject<BookListDto>(rawData["json"] as string);
                var dict = BookList;

                if (dict != null)//&& dict.Count > 0
                {
                    response.StatusCode = "200";
                    response.Data = dict;
                    response.Message = string.Empty;
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy vật phẩm.";
                }

                return response;
            }
            private class BookListDto
            {
                public Guid id { get; set; }
                public string? code { get; set; }
                public double total_price { get; set; }
                public string? status { get; set; }
                public DateTime? created_at { get; set; }
                public List<BookItemDto>? book_items { get; set; }
            }

            private class BookItemDto
            {
                public Guid book_item_id { get; set; }      
                public double current_price { get; set; }
                public string? book_item_status { get; set; }
                public DateTime? book_item_created_at { get; set; }
                public Guid? product_id { get; set; }
                public Guid? product_template_id { get; set; }
                public string? template_product_name { get; set; }
                public string? template_image { get; set; }
                public double template_price { get; set; }
                public double template_rent_price { get; set; }
                public string? template_description { get; set; }
            }
        }
       
    }
}