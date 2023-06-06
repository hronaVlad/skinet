using Models.Account;

namespace Models.Order
{
    public class OrderDto
    {
        public int Id {get; set;}
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }

        public AddressDto ShipToAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public string Status { get; set; }

        public List<OrderItemDto> Items { get; set; }
        public string PaymentIntentId { get; set; }
        public string PaymentClientSecret { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
    }
}
