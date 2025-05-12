using WebApplication4.Repositories.abstractions;

namespace WebApplication4.Repositories;

public class ProductRepository: IProductRepository
{
    private readonly string _connectionString;

    public ProductRepository(IConfiguration cfg)
    {
        _connectionString = cfg.GetConnectionString("Default") ??
                            throw new ArgumentNullException(nameof(cfg),
                                "Default connection string is missing in configuration");
    }
}