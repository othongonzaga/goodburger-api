using goodburger_api.Domain.Enums;

namespace goodburger_api.Domain.Entities;

public sealed class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public SandwichType? Sandwich { get; set; }
    public SideType? Side { get; set; }
    public DrinkType? Drink { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Discount { get; set; }
    public decimal Total { get; set; }
    public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAtUtc { get; set; }
}