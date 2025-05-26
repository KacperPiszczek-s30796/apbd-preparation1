namespace WebApplication4.Repositories.abstractions;

public interface IWarehouseRepository
{
    public Task<bool> WarehouseExistsAsync(int WarehouseId, CancellationToken token = default);
}