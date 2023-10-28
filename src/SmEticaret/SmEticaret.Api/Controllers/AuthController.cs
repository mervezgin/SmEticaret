using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmEticaret.Api.Models;
using SmEticaret.Data;
using SmEticaret.Data.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SmEticaret.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginModel loginModel)
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            
            var user = _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefault(x => x.Email == loginModel.Email);

            if (user is null) 
            {
                return NotFound();
            }

            var hashedPassword = HashString(loginModel.Password);

            if(user.PasswordHash != hashedPassword)
            {
                return NotFound(); 
            }

            var token = GetJwt(user);
            return Ok(new
            {
                Token = token
            });
        }

        private string GetJwt(UserEntity user)
        {
            var claims = new List<Claim>  
            {
                new Claim(JwtClaimTypes.Name, user.Name),
                new Claim(JwtClaimTypes.FamilyName, user.LastName),
                new Claim(JwtClaimTypes.Email, user.Email),
                new Claim(JwtClaimTypes.Role, user.Role.Name ),
            };

            string secret = GetSecretKeyFromConfiguration();
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
            return new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            
        }

        private string GetAudienceFromConfiguration()
        {
            return _configuration["Jwt:Audience"];
        }

        private string GetIssuerFromConfiguration()
        {
            return _configuration["Jwt:Issuer"];
        }

        private string GetSecretKeyFromConfiguration()
        {
            return _configuration["Jwt:Secret"];
        }

        private string HashString(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
    }
}
