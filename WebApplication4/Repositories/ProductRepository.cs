using WebApplication4.Repositories.abstractions;
using System.Data.SqlClient;
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
    public async Task<bool> ProductExistsAsync(int productId, CancellationToken token = default)
    {
        const string query = """
                             SELECT 
                                 IIF(EXISTS (SELECT 1 FROM Product 
                                         WHERE Product.IdProduct = @productId), 1, 0);   
                             """;

        await using SqlConnection con = new(_connectionString);
        await using SqlCommand command = new SqlCommand(query, con);
        await con.OpenAsync(token);
        command.Parameters.AddWithValue("@productId", productId);
        var result = Convert.ToInt32(await command.ExecuteScalarAsync(token));

        return result == 1;
    }
}