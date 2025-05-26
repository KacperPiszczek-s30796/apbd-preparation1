namespace WebApplication4.Repositories.abstractions;

public interface IOrderRepository
{
    public Task<bool> UpdateOrderAsync(int? orderId, CancellationToken token = default);

    public Task<int?> OrderIDAsync(int ProductId, int Amount, DateTime CreatedAt, CancellationToken token = default);
}