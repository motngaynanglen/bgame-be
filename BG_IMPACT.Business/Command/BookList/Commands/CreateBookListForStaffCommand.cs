using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Business.Command.BookList.Commands
{
    public class BookListItemByStaff
    {
        public Guid ProductID { get; set; }
    }
    public class CreateBookListByStaffCommand : IRequest<ResponseObject>
    {
        public Guid? CustomerId { get; set; }
        [Required]
        public List<BookListItemByStaff> BookListItems { get; set; } = [];
        [Required]
        public int FromSlot { get; set; }
        [Required]
        public int ToSlot { get; set; }
        [Required]
        public DateTimeOffset BookDate { get; set; }
        [Required]
        public List<Guid> TableIDs { get; set; } = new List<Guid>();


        public class CreateBookListByStaffCommandHandler : IRequestHandler<CreateBookListByStaffCommand, ResponseObject>
        {
            private readonly IBookListRepository _bookListRepository;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public CreateBookListByStaffCommandHandler(IBookListRepository bookListRepository, IHttpContextAccessor httpContextAccessor)
            {
                _bookListRepository = bookListRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(CreateBookListByStaffCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string? StaffId = null;

                if (context != null && context.GetRole() == "STAFF")
                {
                    StaffId = context.GetName();
                }

                string ProductIDListString = string.Join(",", request.BookListItems.Select(x => x.ProductID));
                string TableIDListString = string.Join(",", request.TableIDs);

                object param = new
                {
                    request.CustomerId,
                    StaffId,

                    request.FromSlot,
                    request.ToSlot,
                    request.BookDate,

                    TableIDListString,
                    ProductIDListString,
                };

                var result = await _bookListRepository.spBookListCreateByStaff(param);
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
                        response.Message = "Có mã hàng không tồn tại";
                    }
                    else if (count == 7)
                    {
                        response.StatusCode = "404";
                        response.Message = "Không có đủ mặt hàng để cho thuê";
                    }
                    else if (count == 8)
                    {
                        response.StatusCode = "404";
                        response.Message = "Dữ liệu đặt hàng nhập vào đơn không đủ";
                    }
                    else if (count == 9)
                    {
                        response.StatusCode = "404";
                        response.Message = "Nhân viên không tồn tại";
                    }
                    else if (count == 10)
                    {
                        response.StatusCode = "404";
                        response.Message = "Nhân viên không thuộc cửa hàng này";
                    }
                    else
                    {
                        response.StatusCode = "200";
                        response.Message = "Đặt hàng thành công";
                        response.Data = dict["id"] as string;
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
