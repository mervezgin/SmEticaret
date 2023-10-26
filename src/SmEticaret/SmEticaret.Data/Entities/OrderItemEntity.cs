using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmEticaret.Data.Entities
{
    public class OrderItemEntity : EntityBase
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        [Required, Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Required, Range(0, int.MaxValue), DataType(DataType.Currency)]
        public decimal Paid { get; set; }
    }
}
