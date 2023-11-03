using Microsoft.AspNetCore.Mvc;
using SmEticaret.Models.Dto;
using SmEticaret.Mvc.Models;
using System.Reflection.Metadata.Ecma335;

namespace SmEticaret.Mvc.Controllers
{
    public class AuthController : Controller
    {
        public AuthController()
        {
            
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login([FromForm] LoginDto loginDto)
        {
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register([FromForm] RegisterViewModel registerDto )
        {
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
    }
}
