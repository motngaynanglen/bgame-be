using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.BookList.Commands
{
    public class ChangeBookListIsPublicFlagCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid Id { get; set; }

        public class ChangeBookListIsPublicFlagCommandHandler : IRequestHandler<ChangeBookListIsPublicFlagCommand, ResponseObject>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;

            private readonly IBookListRepository _bookListRepository;

            public ChangeBookListIsPublicFlagCommandHandler(IBookListRepository bookListRepository, IHttpContextAccessor httpContextAccessor)
            {
                _bookListRepository = bookListRepository;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<ResponseObject> Handle(ChangeBookListIsPublicFlagCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                var context = _httpContextAccessor.HttpContext;

                string UserId = string.Empty;
                string Role = string.Empty;

                if (context != null)
                {
                    UserId = context.GetName();
                    Role = context.GetRole();
                }

                object param = new
                {
                    BookListId = request.Id,
                    UserId,
                    Role
                };

                var result = await _bookListRepository.spBookListChangePublicFlag(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    if (count == 1)
                    {
                        response.StatusCode = "404";
                        response.Message = "Cửa hàng hoặc nhân viên không tồn tại.";
                    }
                    else
                    {
                        response.StatusCode = "200";
                        response.Message = "Đã chỉnh sửa thông tin thành công";
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
