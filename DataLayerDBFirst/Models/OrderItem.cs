namespace DataLayerDBFirst.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }

    public int UserProductId { get; set; }

    public decimal Price { get; set; }

    public string Color { get; set; } = null!;

    public int Count { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual UserProduct UserProduct { get; set; } = null!;
}
