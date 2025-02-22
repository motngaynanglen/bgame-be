using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BG_IMPACT.Jwt
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

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration.GetSection("Keys:JWT").Value));

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
