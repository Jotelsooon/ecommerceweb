using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceWeb.Models;

public class OrderDetail
{
    public int Id { get; set; }

    public int OrderId { get; set; }
    [ForeignKey(nameof(OrderId))]
    public Order? Order { get; set; }

    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public Product? Product { get; set; }

    public int Count { get; set; }
    public double Price { get; set; }
}