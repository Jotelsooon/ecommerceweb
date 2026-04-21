using ECommerceWeb.Models;

namespace ECommerceWeb.DataAccess.Repository;

public interface IProductRepository : IRepository<Product>
{
    void Update(Product product);
}