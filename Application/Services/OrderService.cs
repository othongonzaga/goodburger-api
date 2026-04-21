using goodburger_api.Application.DTOs;
using goodburger_api.Application.Exceptions;
using goodburger_api.Application.Interfaces;
using goodburger_api.Domain.Entities;
using goodburger_api.Domain.ValueObjects;

namespace goodburger_api.Application.Services;

public sealed class OrderService(
    IOrderRepository orderRepository,
    IMenuCatalog menuCatalog,
    IOrderPricingCalculator pricingCalculator) : IOrderService
{
    public async Task<OrderResponse> CreateAsync(UpsertOrderRequest request, CancellationToken cancellationToken)
    {
        var selection = menuCatalog.Parse(request.Items);
        var pricing = pricingCalculator.Calculate(selection);

        var order = new Order
        {
            Sandwich = selection.Sandwich,
            Side = selection.Side,
            Drink = selection.Drink,
            Subtotal = pricing.Subtotal,
            Discount = pricing.Discount,
            Total = pricing.Total
        };

        await orderRepository.AddAsync(order, cancellationToken);
        await orderRepository.SaveChangesAsync(cancellationToken);

        return ToResponse(order);
    }

    public async Task<IReadOnlyCollection<OrderResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetAllAsync(cancellationToken);
        return orders.Select(ToResponse).ToArray();
    }

    public async Task<OrderResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException($"Pedido '{id}' não encontrado.");

        return ToResponse(order);
    }

    public async Task<OrderResponse> UpdateAsync(Guid id, UpsertOrderRequest request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException($"Pedido '{id}' não encontrado.");

        var selection = menuCatalog.Parse(request.Items);
        var pricing = pricingCalculator.Calculate(selection);

        order.Sandwich = selection.Sandwich;
        order.Side = selection.Side;
        order.Drink = selection.Drink;
        order.Subtotal = pricing.Subtotal;
        order.Discount = pricing.Discount;
        order.Total = pricing.Total;
        order.UpdatedAtUtc = DateTime.UtcNow;

        await orderRepository.SaveChangesAsync(cancellationToken);

        return ToResponse(order);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException($"Pedido '{id}' não encontrado.");

        orderRepository.Remove(order);
        await orderRepository.SaveChangesAsync(cancellationToken);
    }

    private OrderResponse ToResponse(Order order)
    {
        var selection = new OrderSelection(order.Sandwich, order.Side, order.Drink);
        var items = menuCatalog.GetItems(selection);
        var pricing = pricingCalculator.Calculate(selection);

        return new OrderResponse(
            order.Id,
            items,
            order.Subtotal,
            pricing.DiscountPercentage,
            order.Discount,
            order.Total,
            order.CreatedAtUtc,
            order.UpdatedAtUtc);
    }
}