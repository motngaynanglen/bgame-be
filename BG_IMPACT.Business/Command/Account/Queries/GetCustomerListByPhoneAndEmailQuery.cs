namespace BG_IMPACT.Business.Command.Account.Queries
{
    public class GetCustomerListByPhoneAndEmailQuery : IRequest<ResponseObject>
    {

        public string Search { get; set; } = string.Empty;
        public class GetCustomerListByPhoneAndEmailQueryHandler : IRequestHandler<GetCustomerListByPhoneAndEmailQuery, ResponseObject>
        {
            private readonly IAccountRepository _accountRepository;
            public GetCustomerListByPhoneAndEmailQueryHandler(IAccountRepository accountRepository)
            {
                _accountRepository = accountRepository;
            }
            public async Task<ResponseObject> Handle(GetCustomerListByPhoneAndEmailQuery request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();
                object param = new
                {
                    request.Search,

                };
                var result = await _accountRepository.spGetCustomerListByPhoneAndEmail(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Guid.TryParse(dict["id"].ToString(), out Guid id))
                {
                    response.StatusCode = "200";
                    response.Data = dict;
                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Không tìm thấy người dùng.";
                }
                return response;
            }
        }
    }
}