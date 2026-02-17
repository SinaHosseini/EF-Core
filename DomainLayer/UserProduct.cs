using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer
{
    public class UserProduct
    {
        public int UserProductId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }

        public required Product Product { get; set; }
    }
}
