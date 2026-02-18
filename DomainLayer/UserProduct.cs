using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainLayer
{
    public class UserProduct
    {
        [Key]
        public int UserProductId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        [NotMapped] //Is that for here not in databse
        [Required]
        [MaxLength(50)]
        public string Color { get; set; }

        [ForeignKey("ProductId")]
        public required Product Product { get; set; }
        [ForeignKey("UserId")]
        public required User User { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
