using goodburger_api.Application.DTOs;
using goodburger_api.Domain.ValueObjects;

namespace goodburger_api.Application.Interfaces;

public interface IMenuCatalog
{
    MenuResponse GetMenu();
    OrderSelection Parse(IReadOnlyCollection<string> itemCodes);
    IReadOnlyCollection<MenuItemResponse> GetItems(OrderSelection selection);
}