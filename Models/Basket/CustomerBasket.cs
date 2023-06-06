namespace Models.Basket
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public List<BasketItem> Items { get; set; }

        public CustomerBasket(string id)
        {
            this.Id = id;
            this.Items = new List<BasketItem>();
        }

        public int DeliveryMethodId { get; set; }
        public string PaymentIntent { get; set; }
    }
}
