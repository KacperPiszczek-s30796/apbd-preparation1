using System.Data.SqlClient;
using WebApplication4.Repositories.abstractions;

namespace WebApplication4.Repositories;

public class WarehouseRepository: IWarehouseRepository
{
    private readonly string _connectionString;

    public WarehouseRepository(IConfiguration cfg)
    {
        _connectionString = cfg.GetConnectionString("Default") ??
                            throw new ArgumentNullException(nameof(cfg),
                                "Default connection string is missing in configuration");
    }
    public async Task<bool> WarehouseExistsAsync(int WarehouseId, CancellationToken token = default)
    {
        const string query = """
                             SELECT 
                                 IIF(EXISTS (SELECT 1 FROM Warehouse 
                                         WHERE Warehouse.IdWarehouse = @WarehouseId), 1, 0);   
                             """;

        await using SqlConnection con = new(_connectionString);
        await using SqlCommand command = new SqlCommand(query, con);
        await con.OpenAsync(token);
        command.Parameters.AddWithValue("@WarehouseId", WarehouseId);
        var result = Convert.ToInt32(await command.ExecuteScalarAsync(token));

        return result == 1;
    }
}