using ECommerceWeb.Models;

namespace ECommerceWeb.DataAccess.Repository;

public interface IShoppingCartRepository : IRepository<ShoppingCart>
{
    void Update(ShoppingCart shoppingCart);
    void IncrementCount(ShoppingCart shoppingCart, int count);
    void DecrementCount(ShoppingCart shoppingCart, int count);
}