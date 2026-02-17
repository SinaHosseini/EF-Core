using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainLayer
{
    public class UserProduct
    {
        public int UserProductId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        [NotMapped] //Is that for here not in databse
        public string Color { get; set; }

        public required Product Product { get; set; }
    }
}
