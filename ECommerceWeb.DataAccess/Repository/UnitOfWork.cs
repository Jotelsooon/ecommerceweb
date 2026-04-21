namespace ECommerceWeb.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;

    public ICategoryRepository Category { get; private set; }
    public IProductRepository Product { get; private set; }
    public IShoppingCartRepository ShoppingCart { get; private set; }
    public IOrderRepository Order { get; private set; }
    public IOrderDetailRepository OrderDetail { get; private set; }

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Category = new CategoryRepository(_db);
        Product = new ProductRepository(_db);
        ShoppingCart = new ShoppingCartRepository(_db);
        Order = new OrderRepository(_db);
        OrderDetail = new OrderDetailRepository(_db);
    }

    public void Save() => _db.SaveChanges();
}