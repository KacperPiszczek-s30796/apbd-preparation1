namespace WebApplication4.Repositories.abstractions;

public interface IProduct_WarehouseRepository
{
    public Task<int?> UpdateOrderAsync(int idWarehouse, int idProduct, int? idOrder, int amount, int Price, CancellationToken token = default);
}