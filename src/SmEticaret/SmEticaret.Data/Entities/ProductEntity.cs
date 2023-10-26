using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmEticaret.Data.Entities
{
    public class ProductEntity : EntityBase
    {
        [Required, MaxLength(50)]
        public string Name { get; set; }

        [Required, Range(0, double.MaxValue), DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Stock { get; set; }

        public int CategoryId { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        public int SellerId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public CategoryEntity Category { get; set; }
         
        [ForeignKey(nameof(SellerId))]
        public UserEntity Seller { get; set; }

    }
}
