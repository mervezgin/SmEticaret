using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace SmEticaret.Data.Entities
{
    public class OrderEntity : EntityBase
    {
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required, MaxLength(250)]
        public string DeliveryAddress { get; set; }

        // Navigation Properties
        public UserEntity User { get; set; }
        public ICollection<OrderItemEntity> OrderItems { get; set; }
    }

    public class OrderEntityConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
