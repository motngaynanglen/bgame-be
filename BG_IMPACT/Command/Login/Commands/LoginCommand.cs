﻿using BG_IMPACT.Models;
using BG_IMPACT.Repositories.Implementations;
using BG_IMPACT.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Rewrite;
using System.ComponentModel.DataAnnotations;

namespace BG_IMPACT.Command.Login.Commands
{
    public class LoginCommand : IRequest<ResponseObject>
    {
        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Tên tài khoản phải từ 8 đến 20 ký tự")]
        public string Username { get; set; } = string.Empty;
        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Mật khẩu phải từ 8 đến 20 ký tự")]
        public string Password { get; set; } = string.Empty;
        public class LoginCommandHandler : IRequestHandler<LoginCommand, ResponseObject>
        {
            private readonly IAccountRepository _accountRepository;
            private readonly IJwtTokenGenerator _jwtTokenGenerator;
            public LoginCommandHandler(IAccountRepository accountRepository, IJwtTokenGenerator jwtTokenGenerator)
            {
                _accountRepository = accountRepository;
                _jwtTokenGenerator = jwtTokenGenerator;
            }

        public async Task<ResponseObject> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                ResponseObject response = new();

                object param = new
                {
                    request.Username,
                    request.Password
                };

                var result = await _accountRepository.spLogin(param);
                var dict = result as IDictionary<string, object>;

                if (dict != null)
                {
                    bool check = Guid.TryParse(dict["id"].ToString(), out _);
                    if (check && dict["id"] != null)
                    {
                        _ = Guid.TryParse(dict["id"].ToString(), out Guid userId);

                        string role = dict["role"].ToString() ?? string.Empty;
                        _ = Guid.TryParse(dict["id"].ToString(), out Guid id);
                        string name = dict["full_name"].ToString() ?? string.Empty;

                        var jwt = _jwtTokenGenerator.GenerateToken(userId, role);
                        var refreshToken = _jwtTokenGenerator.GenerateToken(Guid.NewGuid(), role);

                        object param2 = new
                        {
                            id,
                            refreshToken
                        };

                        var checkSave = await _accountRepository.spAccountAddRefreshToken(param2);

                        response.StatusCode = "200";
                        response.Message = "Đăng nhập thành công";

                        object data = new
                        {
                            jwt,
                            refreshToken,
                            id,
                            name,
                            role
                        };

                        response.Data = data;
                    }
                } 
                else
                {
                    response.StatusCode = "404";
                    response.Message = "Sai tài khoản hoặc mật khẩu";
                }

                return response;
            }
        }
    }
}
