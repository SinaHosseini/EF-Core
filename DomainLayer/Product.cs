using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DomainLayer
{
    public class Product
    {
        //[Key]
        public int ProductId { get; set; }
        //[Required]
        //[MaxLength(80)]
        public string ProductName { get; set; }
        //[Required]
        //[MaxLength(110)]
        public string ImageName { get; set; }
        //[Required(AllowEmptyStrings = false)]
        public string ProductDescription { get; set; }

        public List<UserProduct> UserProducts { get; set; }
    }
}
