using BG_IMPACT.Business.Command.BookList.Commands;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.BookTable.Commands
{
    public class CreateBookTableByCustomerCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid StoreId { get; set; }  
        public DateTimeOffset BookDate { get; set; }
        public int FromSlot { get; set; }
        public int ToSlot { get; set; }
        [Required]
        public List<Guid> TableIDList { get; set; } = new List<Guid>();
        public class CreateBookTableByCustomerCommandHandler : IRequestHandler<CreateBookTableByCustomerCommand, ResponseObject>
        {
            private readonly IHttpContextAccessor _httpContextAccessor;
            private readonly IBookTableRepository _bookTableRepository;
            public CreateBookTableByCustomerCommandHandler(IBookTableRepository bookTableRepository, IHttpContextAccessor httpContextAccessor)
            {
                _bookTableRepository = bookTableRepository;
                _httpContextAccessor = httpContextAccessor;
            }
            public async Task<ResponseObject> Handle(CreateBookTableByCustomerCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                var context = _httpContextAccessor.HttpContext;
                Guid? UserId = null;
                if (context != null && context.GetRole() == "CUSTOMER")
                {
                    _ = Guid.TryParse(context.GetName(), out Guid cusId);
                    UserId = cusId;
                }
                string TableIDListString = string.Join(",", request.TableIDList);
                
                object param = new
                {
                    request.StoreId,
                    request.BookDate,
                    request.FromSlot,
                    request.ToSlot,
                    TableIDListString,
                    UserId
                };
                var result = await _bookTableRepository.spBookTableCreateByCustomer(param);
                var dict = result as IDictionary<string, object>;
                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    if (count != 0)
                    {
                        response.StatusCode = "404";
                        response.Message = "Không thể đặt lịch trùng.";
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
