using ECommerceWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.DataAccess;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderDetail> OrderDetails { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Electrónica", DisplayOrder = 1 },
            new Category { Id = 2, Name = "Ropa", DisplayOrder = 2 },
            new Category { Id = 3, Name = "Hogar", DisplayOrder = 3 }
        );
    }
}