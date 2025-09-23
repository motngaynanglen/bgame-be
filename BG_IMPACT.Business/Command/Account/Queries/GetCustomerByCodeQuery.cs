using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_IMPACT.Business.Command.Account.Queries
{
    public class GetCustomerByCodeQuery : IRequest<ResponseObject>
    {
        public string Code { get; set; }

        public class GetCustomerByCodeQueryHandler : IRequestHandler<GetCustomerByCodeQuery, ResponseObject>
        {
            private readonly IAccountRepository _accountRepository;

            public GetCustomerByCodeQueryHandler(IAccountRepository accountRepository)
            {
                _accountRepository = accountRepository;
            }

            public async Task<ResponseObject> Handle(GetCustomerByCodeQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    Code = request.Code
                };

                var result = await _accountRepository.spCustomerGetByCode(param);
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
