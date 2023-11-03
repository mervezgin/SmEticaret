using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

        public CategoryEntity Category { get; set; }
         
        public UserEntity Seller { get; set; }

        public ICollection<CartItemEntity> CartItems { get; set; }

        public ICollection<OrderItemEntity> OrderItems { get; set; }

        public ICollection<ProductCommentEntity> ProductComments { get; set; }

    }

    public class ProductEntityConfiguration : IEntityTypeConfiguration<ProductEntity>
    {
        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder
                .HasOne(c => c.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder
                .HasOne(c => c.Seller)
                .WithMany(x => x.Products)
                .HasForeignKey(c => c.SellerId)
                .OnDelete(DeleteBehavior.ClientCascade);

        }
    }
}
