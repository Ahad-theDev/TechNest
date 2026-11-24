using TechNest.Models;

namespace TechNest.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> SearchAsync(string term);
    Task<Product?> GetByIdAsync(int id);
    Task<int> AddAsync(Product product);
    Task<bool> UpdateAsync(Product product);
    Task<bool> DeleteAsync(int id);
}