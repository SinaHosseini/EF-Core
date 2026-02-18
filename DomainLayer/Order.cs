using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer
{
    public class Order
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderTime { get; set; }
        public bool IsPay { get; set; }

        public required User User { get; set; }
        public OrderAddress OrderAddress { get; set; }
        public List<OrderItem> OrderItems { get;set; }
    }
}
