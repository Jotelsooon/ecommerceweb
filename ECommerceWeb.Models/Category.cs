using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Orden de visualización")]
    [Range(1, 100)]
    public int DisplayOrder { get; set; }
}