using Dapper;
using TechNest.Data;
using TechNest.Models;

namespace TechNest.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IDbConnectionFactory _factory;

    public ProductRepository(IDbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<IEnumerable<Product>> SearchAsync(string term)
    {
        using var connection = _factory.CreateConnection();
        return await connection.QueryAsync<Product>(
            @"SELECT p.Id, p.Name, p.Description, p.Price, p.Stock, p.CategoryId,
                     c.Name AS CategoryName
              FROM Products p
              INNER JOIN Categories c ON c.Id = p.CategoryId
              WHERE p.Name LIKE '%' + @Term + '%'
                 OR p.Description LIKE '%' + @Term + '%'
              ORDER BY p.Name",
            new { Term = term ?? "" });
    }

    public async Task<Product?> GetByIdAsync(int id)
    {
        using var connection = _factory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Product>(
            @"SELECT p.Id, p.Name, p.Description, p.Price, p.Stock, p.CategoryId,
                     c.Name AS CategoryName
              FROM Products p
              INNER JOIN Categories c ON c.Id = p.CategoryId
              WHERE p.Id = @Id",
            new { Id = id });
    }

    public async Task<int> AddAsync(Product product)
    {
        using var connection = _factory.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(
            @"INSERT INTO Products (Name, Description, Price, Stock, CategoryId)
              OUTPUT INSERTED.Id
              VALUES (@Name, @Description, @Price, @Stock, @CategoryId)",
            product);
    }

    public async Task<bool> UpdateAsync(Product product)
    {
        using var connection = _factory.CreateConnection();
        var rows = await connection.ExecuteAsync(
            @"UPDATE Products
              SET Name = @Name, Description = @Description, Price = @Price,
                  Stock = @Stock, CategoryId = @CategoryId
              WHERE Id = @Id",
            product);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _factory.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "DELETE FROM Products WHERE Id = @Id",
            new { Id = id });
        return rows > 0;
    }
}