using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.BookList.Commands
{
    public class ExtendBookListCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid BookListID { get; set; }
        [Required]
        public DateTimeOffset To { get; set; }
        [Required]
        public int BookType { get; set; }

        public class ExtendBookListCommandHandler : IRequestHandler<ExtendBookListCommand, ResponseObject>
        {
            private readonly IBookListRepository _bookListRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public ExtendBookListCommandHandler(IBookListRepository bookListRepository, IHttpContextAccessor httpContextAccessor)
            {
                _bookListRepository = bookListRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(ExtendBookListCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? StaffId = null;

                if (context != null && context.GetRole() == "STAFF")
                {
                    StaffId = context.GetName();
                }

                object param = new
                {
                    request.BookListID,
                    StaffId,
                    request.To,
                    request.BookType
                };

                var result = await _bookListRepository.spBookListExtend(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    if (count == 1)
                    {
                        response.StatusCode = "404";
                        response.Message = "Phương thức đặt không tồn tại.";
                    }
                    else if (count == 2)
                    {
                        response.StatusCode = "404";
                        response.Message = "Đơn thuê không tồn tại.";
                    }
                    else if (count == 3)
                    {
                        response.StatusCode = "404";
                        response.Message = "Thời hạn thuê không thể trước hạn chót đơn cũ.";
                    }
                    else if (count == 4)
                    {
                        response.StatusCode = "404";
                        response.Message = "Không thể gia hạn đơn thuê theo ngày.";
                    }
                    else if (count == 5)
                    {
                        response.StatusCode = "404";
                        response.Message = "Nhân viên không có quyền hạn ở cửa hàng này.";
                    }
                    else
                    {
                        response.StatusCode = "200";
                        response.Message = "Gia hạn đơn thuê thành công";
                    }

                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Gia hạn đơn thuê thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
    }
}
