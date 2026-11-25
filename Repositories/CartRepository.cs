using Dapper;
using TechNest.Data;
using TechNest.Models;

namespace TechNest.Repositories;

public class CartRepository : ICartRepository
{
    private readonly IDbConnectionFactory _factory;

    public CartRepository(IDbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<IEnumerable<CartItem>> GetByUserAsync(string userId)
    {
        using var connection = _factory.CreateConnection();
        return await connection.QueryAsync<CartItem>(
            @"SELECT ci.Id, ci.UserId, ci.ProductId, ci.Quantity,
                     p.Name AS ProductName, p.Price
              FROM CartItems ci
              INNER JOIN Products p ON p.Id = ci.ProductId
              WHERE ci.UserId = @UserId
              ORDER BY ci.Id",
            new { UserId = userId });
    }

    public async Task AddOrIncreaseAsync(string userId, int productId)
    {
        using var connection = _factory.CreateConnection();
        await connection.ExecuteAsync(
            @"UPDATE CartItems
              SET Quantity = Quantity + 1
              WHERE UserId = @UserId AND ProductId = @ProductId;

              IF @@ROWCOUNT = 0
                  INSERT INTO CartItems (UserId, ProductId, Quantity)
                  VALUES (@UserId, @ProductId, 1);",
            new { UserId = userId, ProductId = productId });
    }

    public async Task<bool> RemoveAsync(string userId, int cartItemId)
    {
        using var connection = _factory.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "DELETE FROM CartItems WHERE Id = @Id AND UserId = @UserId",
            new { Id = cartItemId, UserId = userId });
        return rows > 0;
    }

    public async Task<int> CountAsync(string userId)
    {
        using var connection = _factory.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(
            "SELECT ISNULL(SUM(Quantity), 0) FROM CartItems WHERE UserId = @UserId",
            new { UserId = userId });
    }
}