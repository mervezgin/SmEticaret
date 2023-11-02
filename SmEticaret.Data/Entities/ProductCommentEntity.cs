using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace SmEticaret.Data.Entities
{
    public class ProductCommentEntity : EntityBase
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }

        [Required, MaxLength(250)]
        public string Message { get; set; }

        [Required, Range(1, 5)]
        public byte StarCount { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        public ProductEntity Product { get; set; }

        public UserEntity User { get; set; }
    }

    public class ProductCommentEntityConfiguration : IEntityTypeConfiguration<ProductCommentEntity>
    {
        public void Configure(EntityTypeBuilder<ProductCommentEntity> builder)
        {
            builder.HasOne(pc => pc.Product)
                .WithMany(p => p.ProductComments)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne(pc => pc.User)
                .WithMany(u => u.ProductComments)
                .HasForeignKey(pc => pc.UserId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
