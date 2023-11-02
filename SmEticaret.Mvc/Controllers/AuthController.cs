using Microsoft.AspNetCore.Mvc;
using SmEticaret.Models.Dto;
using SmEticaret.Mvc.Models;
using SmEticaret.Mvc.Services;

namespace SmEticaret.Mvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authService.GetToken(loginDto);

            if (!result.IsSuccess)
            {
                ViewBag.ErrorMessage = "Giriş işlemi başarısız.";
                return View();
            }

            Response.Cookies.Append("jwt", result.Data);

            return RedirectToAction("Index", "Profile");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dto = new RegisterDto
            {
                Email = registerViewModel.Email,
                LastName = registerViewModel.LastName,
                Name = registerViewModel.Name,
                Password = registerViewModel.Password,
                RoleId = registerViewModel.RoleId
            };

            var result = await _authService.CreateUser(dto);

            if (!result.IsSuccess)
            {
                ViewBag.ErrorMessage = "Kayıt işlemi başarısız.";
            }

            ViewBag.SuccessMessage = "Kayıt işlemi başarılı.";

            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
    }
}
