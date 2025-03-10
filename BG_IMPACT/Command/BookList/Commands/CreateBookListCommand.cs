using BG_IMPACT.Command.Product.Commands;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BG_IMPACT.Command.BookList.Commands
{
    public class CreateBookListCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid CustomerId { get; set; }
        [Required]
        public List<Guid> ProductTemplateIds { get; set; } = [];
        [Required]
        public Guid StoreId { get; set; }
        [Required]
        public DateTimeOffset From { get; set; }
        [Required]
        public DateTimeOffset To { get; set; }
        [Required]
        [Range(0, 1, ErrorMessage = "Chỉ được nhập 0 và 1")]
        public int BookType { get; set; }

        public class CreateBookListCommandHandler : IRequestHandler<CreateBookListCommand, ResponseObject>
        {
            private readonly IBookListRepository _bookListRepository;

            public CreateBookListCommandHandler(IBookListRepository bookListRepository)
            {
                _bookListRepository = bookListRepository;
            }

            public async Task<ResponseObject> Handle(CreateBookListCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                string ListProductTemplateId = string.Join(",", request.ProductTemplateIds);

                object param = new
                {
                    request.CustomerId,
                    ListProductTemplateId,
                    request.StoreId,
                    request.From,
                    request.To,
                    request.BookType
                };

                var result = await _bookListRepository.spBookListCreate(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    if (count == 1)
                    {
                        response.StatusCode = "404";
                        response.Message = "Khách hàng không tồn tại.";
                    }
                    else if (count == 2)
                    {
                        response.StatusCode = "404";
                        response.Message = "Phương thức đặt không tồn tại.";
                    }
                    else if (count == 3)
                    {
                        response.StatusCode = "404";
                        response.Message = "Danh sách ID có dữ liệu lạ";
                    }
                    else if (count == 4)
                    {
                        response.StatusCode = "404";
                        response.Message = "Ngày đặt và kết thúc không cùng 1 ngày";
                    }
                    else if (count == 5)
                    {
                        response.StatusCode = "404";
                        response.Message = "Giờ đặt lớn hơn giờ kết thúc";
                    }
                    else if (count == 6)
                    {
                        response.StatusCode = "404";
                        response.Message = "Có ID product group ref không tồn tại";
                    }
                    else if (count == 7)
                    {
                        response.StatusCode = "404";
                        response.Message = "Có product template nhưng không có hàng thuê";
                    }
                    else if (count == 8)
                    {
                        response.StatusCode = "404";
                        response.Message = "Dữ liệu đặt hàng nhập vào đơn không đủ";
                    }
                    else 
                    {
                        response.StatusCode = "200";
                        response.Message = "Đặt hàng thành công";
                    }

                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Thêm sản phẩm thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
    }
}
