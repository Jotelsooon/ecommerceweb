using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceWeb.Models;

public class ShoppingCart
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public Product? Product { get; set; }

    [Range(1, 1000)]
    public int Count { get; set; }

    public string ApplicationUserId { get; set; } = string.Empty;
    [ForeignKey(nameof(ApplicationUserId))]
    public ApplicationUser? ApplicationUser { get; set; }

    [NotMapped]
    public double Price { get; set; }
}