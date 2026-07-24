namespace Ecommerce.Domain.Orders;

public enum OrderStatus
{
    PendingPayment = 1,
    Paid = 2,
    Shipped =3 ,
    Delivered = 4,
    Cancelled = 5
}