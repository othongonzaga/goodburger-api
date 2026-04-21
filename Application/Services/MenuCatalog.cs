using goodburger_api.Application.DTOs;
using goodburger_api.Application.Exceptions;
using goodburger_api.Application.Interfaces;
using goodburger_api.Domain.Enums;
using goodburger_api.Domain.ValueObjects;

namespace goodburger_api.Application.Services;

public sealed class MenuCatalog : IMenuCatalog
{
    private static readonly IReadOnlyDictionary<string, MenuItemResponse> MenuByCode =
        new Dictionary<string, MenuItemResponse>(StringComparer.OrdinalIgnoreCase)
        {
            ["x-burger"] = new("x-burger", "X Burger", "Sanduíche", 5.00m),
            ["x-egg"] = new("x-egg", "X Egg", "Sanduíche", 4.50m),
            ["x-bacon"] = new("x-bacon", "X Bacon", "Sanduíche", 7.00m),
            ["fries"] = new("fries", "Batata frita", "Acompanhamento", 2.00m),
            ["soft-drink"] = new("soft-drink", "Refrigerante", "Bebida", 2.50m)
        };

    private static readonly IReadOnlyDictionary<string, SandwichType> SandwichesByCode =
        new Dictionary<string, SandwichType>(StringComparer.OrdinalIgnoreCase)
        {
            ["x-burger"] = SandwichType.XBurger,
            ["x-egg"] = SandwichType.XEgg,
            ["x-bacon"] = SandwichType.XBacon
        };

    private static readonly IReadOnlyDictionary<string, SideType> SidesByCode =
        new Dictionary<string, SideType>(StringComparer.OrdinalIgnoreCase)
        {
            ["fries"] = SideType.Fries
        };

    private static readonly IReadOnlyDictionary<string, DrinkType> DrinksByCode =
        new Dictionary<string, DrinkType>(StringComparer.OrdinalIgnoreCase)
        {
            ["soft-drink"] = DrinkType.SoftDrink
        };

    private static readonly IReadOnlyDictionary<SandwichType, string> SandwichCodesByType =
        new Dictionary<SandwichType, string>
        {
            [SandwichType.XBurger] = "x-burger",
            [SandwichType.XEgg] = "x-egg",
            [SandwichType.XBacon] = "x-bacon"
        };

    private static readonly IReadOnlyDictionary<SideType, string> SideCodesByType =
        new Dictionary<SideType, string>
        {
            [SideType.Fries] = "fries"
        };

    private static readonly IReadOnlyDictionary<DrinkType, string> DrinkCodesByType =
        new Dictionary<DrinkType, string>
        {
            [DrinkType.SoftDrink] = "soft-drink"
        };

    public MenuResponse GetMenu()
    {
        var sandwiches = SandwichCodesByType.Values.Select(code => MenuByCode[code]).ToArray();
        var sides = SideCodesByType.Values.Select(code => MenuByCode[code]).ToArray();
        var drinks = DrinkCodesByType.Values.Select(code => MenuByCode[code]).ToArray();

        return new MenuResponse(sandwiches, sides, drinks);
    }

    public OrderSelection Parse(IReadOnlyCollection<string> itemCodes)
    {
        if (itemCodes is null || itemCodes.Count == 0)
        {
            throw new DomainValidationException("O pedido deve conter ao menos um item.");
        }

        var normalizedCodes = itemCodes
            .Where(code => !string.IsNullOrWhiteSpace(code))
            .Select(code => code.Trim())
            .ToList();

        if (normalizedCodes.Count == 0)
        {
            throw new DomainValidationException("O pedido deve conter ao menos um item válido.");
        }

        var duplicate = normalizedCodes
            .GroupBy(code => code, StringComparer.OrdinalIgnoreCase)
            .FirstOrDefault(group => group.Count() > 1);

        if (duplicate is not null)
        {
            throw new DomainValidationException($"O item '{duplicate.Key}' foi informado mais de uma vez.");
        }

        SandwichType? sandwich = null;
        SideType? side = null;
        DrinkType? drink = null;

        foreach (var code in normalizedCodes)
        {
            if (SandwichesByCode.TryGetValue(code, out var sandwichType))
            {
                if (sandwich.HasValue)
                {
                    throw new DomainValidationException("O pedido permite apenas um sanduíche.");
                }

                sandwich = sandwichType;
                continue;
            }

            if (SidesByCode.TryGetValue(code, out var sideType))
            {
                if (side.HasValue)
                {
                    throw new DomainValidationException("O pedido permite apenas uma batata.");
                }

                side = sideType;
                continue;
            }

            if (DrinksByCode.TryGetValue(code, out var drinkType))
            {
                if (drink.HasValue)
                {
                    throw new DomainValidationException("O pedido permite apenas um refrigerante.");
                }

                drink = drinkType;
                continue;
            }

            throw new DomainValidationException($"O item '{code}' não existe no cardápio.");
        }

        return new OrderSelection(sandwich, side, drink);
    }

    public IReadOnlyCollection<MenuItemResponse> GetItems(OrderSelection selection)
    {
        var items = new List<MenuItemResponse>();

        if (selection.Sandwich.HasValue)
        {
            items.Add(MenuByCode[SandwichCodesByType[selection.Sandwich.Value]]);
        }

        if (selection.Side.HasValue)
        {
            items.Add(MenuByCode[SideCodesByType[selection.Side.Value]]);
        }

        if (selection.Drink.HasValue)
        {
            items.Add(MenuByCode[DrinkCodesByType[selection.Drink.Value]]);
        }

        return items;
    }
}