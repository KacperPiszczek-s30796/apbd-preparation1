using System.Data.SqlClient;
using WebApplication4.Repositories.abstractions;

namespace WebApplication4.Repositories;

public class Product_WarehouseRepository: IProduct_WarehouseRepository
{
    private readonly string _connectionString;

    public Product_WarehouseRepository(IConfiguration cfg)
    {
        _connectionString = cfg.GetConnectionString("Default") ??
                            throw new ArgumentNullException(nameof(cfg),
                                "Default connection string is missing in configuration");
    }
    public async Task<int?> UpdateOrderAsync(int idWarehouse, int idProduct, int? idOrder, int amount,int Price, CancellationToken token = default)
    {
        const string query = """
                             INSERT INTO Product_Warehouse (idWarehouse, idProduct, idOrder, amount, Price, CreatedAt)
                             VALUES (@idWarehouse, @idProduct, @idOrder, @amount, @Price, @CreatedAt)
                             SELECT CAST(SCOPE_IDENTITY() AS int);
                             """;
        await using SqlConnection con = new(_connectionString);
        await using SqlCommand command = new SqlCommand(query, con);
        await con.OpenAsync(token);
        command.Parameters.AddWithValue("@idWarehouse", idWarehouse);
        command.Parameters.AddWithValue("@idProduct", idProduct);
        command.Parameters.AddWithValue("@IdOrder", idOrder);
        command.Parameters.AddWithValue("@amount", amount);
        command.Parameters.AddWithValue("@Price", Price);
        command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
        int? affectedRows = null;
        try
        {
            affectedRows = Convert.ToInt32(await command.ExecuteScalarAsync(token));
        }
        catch (SqlException ex)
        {
            return null;
        }
        return affectedRows;
    }
    
}