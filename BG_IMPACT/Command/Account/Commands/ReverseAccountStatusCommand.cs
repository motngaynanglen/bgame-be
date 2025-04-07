using BG_IMPACT.Extensions;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;

namespace BG_IMPACT.Command.Account.Commands
{
    public class ReverseAccountStatusCommand
    {
        public Guid? AccountID { get; set; }

        public class ReverseStaffStatusCommandCommandHandler : IRequestHandler<ReverseStaffStatusCommand, ResponseObject>
        {
            private readonly IAccountRepository _accountRepository;


            public ReverseStaffStatusCommandCommandHandler(IAccountRepository accountRepository)
            {
                _accountRepository = accountRepository;
            }

            public async Task<ResponseObject> Handle(ReverseStaffStatusCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object parameters = new
                {
                    request.AccountID
                };
                var result = await _accountRepository.spAccountReverseStatusForAdmin(parameters);
                var dict = result as IDictionary<string, object>;

                if (dict != null && Int64.TryParse(dict["Status"].ToString(), out _) == true)
                {
                    _ = Int64.TryParse(dict["Status"].ToString(), out long count);

                    string? Message = dict["Message"].ToString() ?? string.Empty;

                    if (count == 1)
                    {
                        response.StatusCode = "404";
                        response.Message = Message;
                    }
                    else if (count == 2)
                    {
                        response.StatusCode = "404";
                        response.Message = Message;
                    }

                    else
                    {
                        response.StatusCode = "200";
                        response.Message = Message;
                    }

                }
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Cập nhật thông tin thất bại. Xin hãy thử lại sau.";
                }

                return response;
            }
        }
    }

}
