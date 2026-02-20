using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainLayer
{
    public class OrderAddress
    {
        //[Key]
        public int OrderAddressId { get; set; }
        public int OrderId { get; set; }
        //[Required]
        //[MaxLength(100)]
        public string City { get; set; }
        //[Required(AllowEmptyStrings = false)]
        //[MaxLength(1000)]
        public string Address { get; set; }

        //[ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}
