namespace DataLayerDBFirst.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public DateTime OrderTime { get; set; }

    public bool IsPay { get; set; }

    public virtual OrderAddress? OrderAddress { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User User { get; set; } = null!;
}
