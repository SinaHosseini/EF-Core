using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int UserProductId { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public int Count { get; set; }
    }
}
