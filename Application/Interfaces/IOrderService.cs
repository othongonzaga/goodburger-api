using goodburger_api.Application.DTOs;

namespace goodburger_api.Application.Interfaces;

public interface IOrderService
{
    Task<OrderResponse> CreateAsync(UpsertOrderRequest request, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<OrderResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<OrderResponse> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<OrderResponse> UpdateAsync(Guid id, UpsertOrderRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}