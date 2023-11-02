using System.ComponentModel.DataAnnotations;

namespace SmEticaret.Models.Dto
{
    public class RegisterDto
    {
        public int RoleId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(1)]
        public string Password { get; set; }
    }
}
