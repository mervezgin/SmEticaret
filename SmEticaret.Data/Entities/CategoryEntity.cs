using System.ComponentModel.DataAnnotations;

namespace SmEticaret.Data.Entities
{
    public class CategoryEntity : EntityBase
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public ICollection<ProductEntity> Products { get; set; }
    }
}
