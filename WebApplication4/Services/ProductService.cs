using WebApplication4.contracts.requests;
using WebApplication4.entities;
using WebApplication4.Repositories.abstractions;
using WebApplication4.Services.abstractions;

namespace WebApplication4.Services;

public class ProductService:IProductService
{
    private readonly IProductRepository productRepository;
    private readonly IWarehouseRepository warehouseRepository;
    private readonly IOrderRepository orderRepository;
    private readonly IProduct_WarehouseRepository product_WarehouseRepository;
    public ProductService(IProductRepository repository, IWarehouseRepository repository2, IOrderRepository orderRepository, IProduct_WarehouseRepository product_WarehouseRepository)
    {
        this.productRepository = repository;
        this.warehouseRepository = repository2;
        this.orderRepository = orderRepository;
        this.product_WarehouseRepository = product_WarehouseRepository;
    }

    public async Task<int?> RealizeOrder(CreateProductWarehouse createProductWarehouse,
        CancellationToken token = default)
    {
        int? result = null;
        bool firstcheck = await productRepository.ProductExistsAsync(createProductWarehouse.IdProduct, token);
        bool secondcheck = await warehouseRepository.WarehouseExistsAsync(createProductWarehouse.IdWarehouse, token);
        if (firstcheck && secondcheck && createProductWarehouse.Amount > 0)
        {
            int? orderid = await orderRepository.OrderIDAsync(createProductWarehouse.IdProduct,createProductWarehouse.Amount, DateTime.Now, token);
            if (orderid != null)
            {
                bool updateStatus = await orderRepository.UpdateOrderAsync(orderid, token);
                if (updateStatus)
                {
                    int price = await productRepository.ProductGetPriceAsync(createProductWarehouse.IdProduct, token);
                    result = await product_WarehouseRepository.UpdateOrderAsync(createProductWarehouse.IdWarehouse, createProductWarehouse.IdProduct, orderid, createProductWarehouse.Amount, price*createProductWarehouse.Amount, token);
                }
            }
        }
        return result;
    }
}