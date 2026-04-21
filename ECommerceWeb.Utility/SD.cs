namespace ECommerceWeb.Utility;

public static class SD
{
    // Roles
    public const string Role_Customer = "Customer";
    public const string Role_Admin = "Admin";
    public const string Role_Employee = "Employee";

    // Estados de pedido
    public const string StatusPending = "Pending";
    public const string StatusApproved = "Approved";
    public const string StatusProcessing = "Processing";
    public const string StatusShipped = "Shipped";
    public const string StatusCancelled = "Cancelled";
    public const string StatusRefunded = "Refunded";

    // Estados de pago
    public const string PaymentStatusPending = "Pending";
    public const string PaymentStatusApproved = "Approved";
    public const string PaymentStatusRejected = "Rejected";
}