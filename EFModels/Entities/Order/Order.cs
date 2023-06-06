namespace EFModels.Entities.Order
{
    public class Order : EntityKey
    {
        public Order()
        {
        }

        public Order(string buyerEmail, Address shipToAddress, DeliveryMethod deliveryMethod, List<OrderItem> items, string paymentIntentId, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            PaymentIntentId = paymentIntentId;
            SubTotal = subTotal;
        }

        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        public Address ShipToAddress { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Panding;
        
        public List<OrderItem> Items { get; set; }
        public string PaymentIntentId { get; set; }
        public string PaymentClientSecret { get; set; }
        public decimal SubTotal { get; set; }

        public int DeliveryMethodId { get; set; }

        public decimal GetTotal() => SubTotal + DeliveryMethod.Price;
    }
}
