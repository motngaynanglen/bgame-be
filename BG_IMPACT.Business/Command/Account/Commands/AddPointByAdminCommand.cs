using Azure;
using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Account.Commands
{
    public class AddPointByAdminCommand : IRequest<ResponseObject>
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public int Point {  get; set; }

        public class AddPointByAdminCommandHandler : IRequestHandler<AddPointByAdminCommand, ResponseObject>
        {
            private readonly ICustomerRepository _customerRepository;

            public AddPointByAdminCommandHandler(ICustomerRepository customerRepository)
            {
                _customerRepository = customerRepository;
            }

            public async Task<ResponseObject> Handle(AddPointByAdminCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.UserId,
                    request.Point
                };

                var result = await _customerRepository.spCustomerAddPointByAdmin(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    if (count == 1)
                    {
                        response.StatusCode = "404";
                        response.Message = "Khách hàng không tồn tại.";
                    }
                    else
                    {
                        response.StatusCode = "200";
                        response.Message = "Thêm điểm thành công.";
                    }
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Thêm điểm thất bại.";
                }

                return response;
            }
        }
    }
}
