using goodburger_api.Domain.Entities;

namespace goodburger_api.Application.Interfaces;

public interface IOrderRepository
{
    Task AddAsync(Order order, CancellationToken cancellationToken);
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<Order>> GetAllAsync(CancellationToken cancellationToken);
    void Remove(Order order);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}