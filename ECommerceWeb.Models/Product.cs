using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceWeb.Models;

public class Product
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    [Required]
    [Range(0.01, 10000)]
    public double Price { get; set; }

    public string? ImageUrl { get; set; }

    public int CategoryId { get; set; }
    [ForeignKey(nameof(CategoryId))]
    public Category? Category { get; set; }
}