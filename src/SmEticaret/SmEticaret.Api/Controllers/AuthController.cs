using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SmEticaret.Api.Models;
using SmEticaret.Api.Services.TokenService;
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
        private readonly ITokenService _tokenService;

        public AuthController(AppDbContext dbContext, IConfiguration configuration, ITokenService tokenService)
        {
            _dbContext = dbContext;
            _configuration = configuration;
            _tokenService = tokenService;
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

            var tokenResult = _tokenService.CreateToken(user);

            if(!tokenResult.IsSuccess) 
            {
                return StatusCode(tokenResult.StatusCode, tokenResult.Message);
            }
            return Ok(new
            {
                Token = tokenResult.Data
            });
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _dbContext.Users.SingleOrDefaultAsync(x => x.Email == registerModel.Email);

            if(user is not null)
            {
                return BadRequest("Bu mail adresi kullanılmaktadır");
            }

            var newUser = new UserEntity
            {
                Name = registerModel.Name,
                LastName = registerModel.LastName,
                Email = registerModel.Email,
                PasswordHash = HashString(registerModel.Password),
                RoleId = registerModel.RoleId
            };

            _dbContext.Users.Add(newUser);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
        private string HashString(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
    }
}
