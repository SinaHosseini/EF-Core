namespace DataLayerDBFirst.Models;

public partial class OrderAddress
{
    public int OrderAddressId { get; set; }

    public int OrderId { get; set; }

    public string City { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
