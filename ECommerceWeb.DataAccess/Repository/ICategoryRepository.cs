using ECommerceWeb.Models;

namespace ECommerceWeb.DataAccess.Repository;

public interface ICategoryRepository : IRepository<Category>
{
    void Update(Category category);
}