namespace DataLayerDBFirst.Models;

public partial class UserProduct
{
    public int UserProductId { get; set; }

    public int UserId { get; set; }

    public int ProductId { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Product Product { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
