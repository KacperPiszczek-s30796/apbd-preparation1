using WebApplication4.contracts.requests;

namespace WebApplication4.Services.abstractions;

public interface IProductService
{
    public Task<int?> RealizeOrder(CreateProductWarehouse createProductWarehouse, CancellationToken token = default);
}