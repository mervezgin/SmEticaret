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
    public class CartItemEntity : EntityBase
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public CartEntity Cart { get; set; }

        public ProductEntity Product { get; set; }
    }

    public class CartItemEntityConfiguration : IEntityTypeConfiguration<CartItemEntity>
    {
        public void Configure(EntityTypeBuilder<CartItemEntity> builder)
        {
            builder
                .HasOne(c => c.Cart)
                .WithMany(u => u.CartItems)
                .HasForeignKey(c => c.CartId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder
                .HasOne(c => c.Product)
                .WithMany(x => x.CartItems)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
