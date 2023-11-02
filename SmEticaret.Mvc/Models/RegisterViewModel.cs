using System.ComponentModel.DataAnnotations;

namespace SmEticaret.Mvc.Models
{
    public class RegisterViewModel
    {
        [Required]
        public int RoleId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(1), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, MinLength(1), Compare(nameof(Password)), DataType(DataType.Password)]
        public string PasswordRepeat { get; set; }
    }
}
