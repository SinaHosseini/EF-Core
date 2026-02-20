using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DomainLayer
{
    public class UserProduct
    {
        private Product _product;
        private ILazyLoader _lazyLoaderl;

        public UserProduct()
        {

        }

        public UserProduct(ILazyLoader lazyLoader)
        {
            _lazyLoaderl = lazyLoader;
        }
        //[Key]
        public int UserProductId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        //[NotMapped] //Is that for here not in databse
        //[Required]
        //[MaxLength(50)]
        public string Color { get; set; }

        //[ForeignKey("ProductId")]
        public Product Product
        {
            get => _lazyLoaderl.Load(this, ref _product);
            set => _product = value;
        }
        //[ForeignKey("UserId")]
        public virtual User User { get; set; }
        public virtual List<OrderItem> OrderItems { get; set; }
    }
}
