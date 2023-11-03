using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmEticaret.Data.Entities
{
    public class CategoryEntity : EntityBase
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public ICollection<ProductEntity> Products { get; set; }
    }
}
