namespace ECommerceWeb.DataAccess.Repository;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }
    IProductRepository Product { get; }
    IShoppingCartRepository ShoppingCart { get; }
    IOrderRepository Order { get; }
    IOrderDetailRepository OrderDetail { get; }
    void Save();
}