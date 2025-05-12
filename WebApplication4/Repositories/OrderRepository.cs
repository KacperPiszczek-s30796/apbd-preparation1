using System.Data.SqlClient;
using WebApplication4.entities;
using WebApplication4.Repositories.abstractions;

namespace WebApplication4.Repositories;

public class OrderRepository: IOrderRepository
{
    private readonly string _connectionString;

    public OrderRepository(IConfiguration cfg)
    {
        _connectionString = cfg.GetConnectionString("Default") ??
                            throw new ArgumentNullException(nameof(cfg),
                                "Default connection string is missing in configuration");
    }

    public async Task<bool> UpdateOrderAsync(int? orderId, CancellationToken token = default)
    {
        const string query = """
                             UPDATE Order
                                SET FulfilledAt = @FulfilledAt
                                WHERE IdOrder = @IdOrder;
                            """;
        await using SqlConnection con = new(_connectionString);
        await using SqlCommand command = new SqlCommand(query, con);
        await con.OpenAsync(token);
        command.Parameters.AddWithValue("@IdOrder", orderId);
        command.Parameters.AddWithValue("@FulfilledAt", DateTime.Now);
        try
        {
            int affectedRows = await command.ExecuteNonQueryAsync(token);
            if (affectedRows == 0)
            {
                return false;
            }
        }
        catch (SqlException ex)
        {
            return false;
        }

        return true;
    }
    public async Task<int?> OrderIDAsync(int ProductId,int Amount,DateTime CreatedAt, CancellationToken token = default)
    {
        const string query = """
                             SELECT TOP 1 Order.IdOrder FROM Order 
                             WHERE Order.IdProduct = @ProductId and Order.Amount = @Amount and Order.CreatedAt<@CreatedAt and Order.FulfilledAt is NULL;   
                             """;

        await using SqlConnection con = new(_connectionString);
        await using SqlCommand command = new SqlCommand(query, con);
        await con.OpenAsync(token);
        command.Parameters.AddWithValue("@ProductId", ProductId);
        command.Parameters.AddWithValue("@Amount", Amount);
        command.Parameters.AddWithValue("@CreatedAt", CreatedAt);
        int? result;
        try
        {
            result = Convert.ToInt32(await command.ExecuteScalarAsync(token));
        }
        catch (Exception ex)
        {
            result = null;
        }

        const string query2 = """
                              SELECT 
                              IIF(EXISTS (SELECT 1 FROM Product_Warehouse
                              WHERE Order.IdOrder = @result),1,0);
                              """;
        await using SqlCommand command2 = new SqlCommand(query2, con);
        await con.OpenAsync(token);
        command2.Parameters.AddWithValue("@result", result);
        var result2 = Convert.ToInt32(await command2.ExecuteScalarAsync(token));
        if (result2 == 1) return null;
        else if (result is not null)
        {
            if (!Convert.ToBoolean(UpdateOrderAsync(result, token))) result = null;
        }
        return result;
    }
}