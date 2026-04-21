using ECommerceWeb.Models;

namespace ECommerceWeb.DataAccess.Repository;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    private readonly ApplicationDbContext _db;

    public OrderRepository(ApplicationDbContext db) : base(db)
        => _db = db;

    public void Update(Order order) => _db.Orders.Update(order);

    public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
    {
        var order = _db.Orders.FirstOrDefault(o => o.Id == id);
        if (order != null)
        {
            order.OrderStatus = orderStatus;
            if (paymentStatus != null)
                order.PaymentStatus = paymentStatus;
        }
    }

    public void UpdateStripePaymentId(int id, string sessionId, string? paymentIntentId)
    {
        var order = _db.Orders.FirstOrDefault(o => o.Id == id);
        if (order != null)
        {
            if (!string.IsNullOrEmpty(sessionId))
                order.SessionId = sessionId;
            if (!string.IsNullOrEmpty(paymentIntentId))
                order.PaymentIntentId = paymentIntentId;
        }
    }
}