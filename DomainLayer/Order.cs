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
        public OrderStatus Status { get; set; }

        public virtual User User { get; set; }
        public virtual OrderAddress OrderAddress { get; set; }
        public virtual List<OrderItem> OrderItems { get;set; }
    }

    public enum OrderStatus
    {
        IsPay,
        Canceled,
        Finally
    }
}
