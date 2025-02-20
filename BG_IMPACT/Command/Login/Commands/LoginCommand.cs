﻿using BG_IMPACT.Repositories.Implementations;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;

namespace BG_IMPACT.Command.Login.Commands
{
    public class LoginCommand : IRequest<object>
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public class LoginCommandHandler : IRequestHandler<LoginCommand, object>
        {
            private IAccountRepository _accountRepository;
            public LoginCommandHandler(IAccountRepository accountRepository)
            {
                _accountRepository = accountRepository;
            }

            public async Task<object> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                object param = new
                {
                    request.Username,
                    request.Password
                };

                var result = await _accountRepository.spLogin(param);

                return result ?? "Sai tài khoản hoặc mật khẩu";
            }
        }
    }
}
