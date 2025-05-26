namespace WebApplication4.Repositories.abstractions;

public interface IProductRepository
{
    public Task<bool> ProductExistsAsync(int productId, CancellationToken token = default);
    public Task<int> ProductGetPriceAsync(int productId, CancellationToken token = default);
}