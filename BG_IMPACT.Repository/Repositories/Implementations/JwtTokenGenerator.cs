﻿using BG_IMPACT.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace BG_IMPACT.Repositories.Implementations
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator()
        {
            ConfigurationBuilder configurationBuilder = new();

            _ = configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = configurationBuilder.Build();
            _configuration = configuration;
        }
        public string GenerateToken(Guid id, string role)
        {
            Claim[] claims =
            [
                new Claim(ClaimTypes.Name, id.ToString()),
                new Claim(ClaimTypes.Role, role.ToString())
            ];

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration.GetSection("Keys:JWT").Value ?? string.Empty));

            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken token = new(
                claims: claims,
                expires: DateTime.Now.AddHours(24),
                signingCredentials: creds);

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
