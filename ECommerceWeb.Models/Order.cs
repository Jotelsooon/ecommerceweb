using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceWeb.Models;

public class Order
{
    public int Id { get; set; }

    public string ApplicationUserId { get; set; } = string.Empty;
    [ForeignKey(nameof(ApplicationUserId))]
    public ApplicationUser? ApplicationUser { get; set; }

    public DateTime OrderDate { get; set; }
    public double OrderTotal { get; set; }

    public string? OrderStatus { get; set; }
    public string? PaymentStatus { get; set; }

    public string? SessionId { get; set; }
    public string? PaymentIntentId { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required]
    public string StreetAddress { get; set; } = string.Empty;
    [Required]
    public string City { get; set; } = string.Empty;
    [Required]
    public string State { get; set; } = string.Empty;
    [Required]
    public string PostalCode { get; set; } = string.Empty;

    public IEnumerable<OrderDetail>? OrderDetails { get; set; }
}