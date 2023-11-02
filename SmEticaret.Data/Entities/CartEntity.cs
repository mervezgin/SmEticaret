using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SmEticaret.Data.Entities
{
    public class CartEntity : EntityBase
    {
        public int UserId { get; set; }

        // Navigation Properties

        public UserEntity User { get; set; }
        public ICollection<CartItemEntity> CartItems { get; set; }
    }

    public class CartEntityConfiguration : IEntityTypeConfiguration<CartEntity>
    {
        public void Configure(EntityTypeBuilder<CartEntity> builder)
        {
            builder.HasOne(c => c.User)
                .WithMany(u => u.Carts)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
