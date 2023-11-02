using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using SmEticaret.Data.Entities;
using SmEticaret.Shared.ServiceResult;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmEticaret.Api.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IServiceResult<string> CreateToken(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Name, user.Name),
                new Claim(JwtClaimTypes.FamilyName, user.LastName),
                new Claim(JwtClaimTypes.Email, user.Email),
                new Claim(JwtClaimTypes.Role, user.Role.Name)
            };

            string? secret = GetSecretKeyFromConfiguration();
            if (string.IsNullOrWhiteSpace(secret))
            {
                return ServiceResult.Fail<string>("Secret key not found in config", StatusCodes.Status501NotImplemented);
            }

            string issuer = GetIssuerFromConfiguration();
            string audience = GetAudienceFromConfiguration();

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return ServiceResult.Success(token);
        }

        private string GetAudienceFromConfiguration()
        {
            return _configuration["Jwt:Audience"];
        }

        private string GetIssuerFromConfiguration()
        {
            return _configuration["Jwt:Issuer"];
        }

        private string? GetSecretKeyFromConfiguration()
        {
            return _configuration["Jwt:Secret"];
        }
    }
}
