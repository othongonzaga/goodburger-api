using goodburger_api.Domain.Enums;

namespace goodburger_api.Domain.ValueObjects;

public sealed record OrderSelection(
    SandwichType? Sandwich,
    SideType? Side,
    DrinkType? Drink)
{
    public bool HasSandwich => Sandwich.HasValue;
    public bool HasSide => Side.HasValue;
    public bool HasDrink => Drink.HasValue;
}