using ECommerceWeb.Models;

namespace ECommerceWeb.DataAccess.Repository;

public interface IOrderDetailRepository : IRepository<OrderDetail>
{
    void Update(OrderDetail orderDetail);
}