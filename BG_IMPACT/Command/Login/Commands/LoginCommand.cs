using BG_IMPACT.Jwt;
using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Implementations;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;

namespace BG_IMPACT.Command.Login.Commands
{
    public class LoginCommand : IRequest<ResponseObject>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public class LoginCommandHandler : IRequestHandler<LoginCommand, ResponseObject>
        {
            private IAccountRepository _accountRepository;
            private IJwtTokenGenerator _jwtTokenGenerator;
            public LoginCommandHandler(IAccountRepository accountRepository, IJwtTokenGenerator jwtTokenGenerator)
            {
                _accountRepository = accountRepository;
                _jwtTokenGenerator = jwtTokenGenerator;
            }

        public async Task<ResponseObject> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                object param = new
                {
                    request.Username,
                    request.Password
                };

                var result = await _accountRepository.spLogin(param);
                var dict = result as IDictionary<string, object>;

                bool check = Guid.TryParse(dict["id"].ToString(), out Guid userId);
                string role = dict["role"].ToString() ?? "";
                _ = Guid.TryParse(dict["id"].ToString(), out Guid id);

                var jwt = _jwtTokenGenerator.GenerateToken(userId, role);
                var refreshToken = _jwtTokenGenerator.GenerateToken(Guid.NewGuid(), role);

                object param2 = new
                {
                    id,
                    refreshToken
                };

                var checkSave = await _accountRepository.spAccountAddRefreshToken(param2);

                ResponseObject response = new();

                if (result == null)
                {
                    response.StatusCode = "404";
                    response.Message = "Sai tài khoản hoặc mật khẩu";
                } 
                else 
                {
                    response.StatusCode = "200";
                    response.Message = "Đăng nhập thành công";

                    object data = new
                    {
                        jwt,
                        refreshToken
                    };

                    response.Data = data;
                }
                
                return response;
            }
        }
    }
}
