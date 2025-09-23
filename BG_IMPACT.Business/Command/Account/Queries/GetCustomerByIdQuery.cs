using CloudinaryDotNet.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Account.Queries
{
    public class GetCustomerByIdQuery : IRequest<ResponseObject>
    {
        public Guid Id;

        public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, ResponseObject>
        {
            private readonly IAccountRepository _accountRepository;

            public GetCustomerByIdQueryHandler(IAccountRepository accountRepository)
            {
                _accountRepository = accountRepository;
            }

            public async Task<ResponseObject> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    CustomerID = request.Id
                };

                var result = await _accountRepository.spCustomerGetById(param);
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
                    response.Message = "Không tìm thấy thông tin người dùng.";
                }

                return response;
            }
        }
    }
}
