using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainLayer
{
    public class OrderItem
    {
        //[Key]
        public int OrderItemId { get; set; }
        // [ForeignKey("Order")]
        public int OrderId { get; set; }
        public int UserProductId { get; set; }
        public decimal Price { get; set; }
        //[Required]
        //[MaxLength(50)]
        public string Color { get; set; }
        public int Count { get; set; }

        //[ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        //[ForeignKey("UserProductId")]
        public virtual UserProduct UserProduct { get; set; }
    }
}
