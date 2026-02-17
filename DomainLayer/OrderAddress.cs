using System;
using System.Collections.Generic;
using System.Text;

namespace DomainLayer
{
    public class OrderAddress
    {
        public int OrderAddressId { get; set; }
        public int OrderId { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public required Order Order { get; set; }
    }
}
