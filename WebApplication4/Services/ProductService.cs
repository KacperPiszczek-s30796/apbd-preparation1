using WebApplication4.Repositories.abstractions;
using WebApplication4.Services.abstractions;

namespace WebApplication4.Services;

public class ProductService:IProductService
{
    private readonly IProductRepository productRepository;
    public ProductService(IProductRepository repository)
    {
        productRepository = repository;
    }
    
}