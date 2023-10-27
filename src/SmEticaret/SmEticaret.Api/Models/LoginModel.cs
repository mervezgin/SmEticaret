using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace SmEticaret.Api.Models
{
    public class LoginModel
    {
        [Required, MinLength(1), EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(1)]
        public string Password { get; set; }
    }
}
