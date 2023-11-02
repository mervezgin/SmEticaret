using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace SmEticaret.Data.Entities
{
    public class CartItemEntity : EntityBase
    {
        public int CartId { get; set; }

        public int ProductId { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        // Navigation Properties
        public CartEntity Cart { get; set; }
        public ProductEntity Product { get; set; }
    }

    public class CartItemEntityConfiguration : IEntityTypeConfiguration<CartItemEntity>
    {
        public void Configure(EntityTypeBuilder<CartItemEntity> builder)
        {
            builder.HasOne(c => c.Cart)
                .WithMany(x => x.CartItems)
                .HasForeignKey(c => c.CartId)
                .OnDelete(DeleteBehavior.ClientCascade);


            builder.HasOne(c => c.Product)
                .WithMany(x => x.CartItems)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
