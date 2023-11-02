using System.ComponentModel.DataAnnotations;

namespace SmEticaret.Data.Entities
{
    public class RoleEntity : EntityBase
    {
        [Required, MaxLength(10)]
        public string Name { get; set; }
        public ICollection<UserEntity> Users { get; set; }
    }
}
