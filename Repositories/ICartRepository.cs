using TechNest.Models;

namespace TechNest.Repositories;

public interface ICartRepository
{
    Task<IEnumerable<CartItem>> GetByUserAsync(string userId);
    Task AddOrIncreaseAsync(string userId, int productId);
    Task<bool> RemoveAsync(string userId, int cartItemId);
    Task<int> CountAsync(string userId);
}