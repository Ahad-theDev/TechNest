using Dapper;
using TechNest.Data;
using TechNest.Models;

namespace TechNest.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly IDbConnectionFactory _factory;

    public CategoryRepository(IDbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        using var connection = _factory.CreateConnection();
        return await connection.QueryAsync<Category>(
            "SELECT Id, Name, Description FROM Categories ORDER BY Name");
    }

    public async Task<Category?> GetByIdAsync(int id)
    {
        using var connection = _factory.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Category>(
            "SELECT Id, Name, Description FROM Categories WHERE Id = @Id",
            new { Id = id });
    }

    public async Task<int> AddAsync(Category category)
    {
        using var connection = _factory.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(
            @"INSERT INTO Categories (Name, Description)
              OUTPUT INSERTED.Id
              VALUES (@Name, @Description)",
            category);
    }

    public async Task<bool> UpdateAsync(Category category)
    {
        using var connection = _factory.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "UPDATE Categories SET Name = @Name, Description = @Description WHERE Id = @Id",
            category);
        return rows > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var connection = _factory.CreateConnection();
        var rows = await connection.ExecuteAsync(
            "DELETE FROM Categories WHERE Id = @Id",
            new { Id = id });
        return rows > 0;
    }
}