using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmEticaret.Data.Entities
{
    public class ProductCommentEntity : EntityBase
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }

        [Required, MaxLength(250)]
        public string Message { get; set; }

        [Required, Range(1,5)]
        public byte StarCount { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
