using System.ComponentModel.DataAnnotations;

namespace SmEticaret.Models.Dto
{
    public class LoginDto
    {
        [Required, MinLength(1), EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(1), DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
