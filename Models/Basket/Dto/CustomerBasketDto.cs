namespace Models.Basket.Dto
{
    public class CustomerBasketDto
    {
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }

        public CustomerBasketDto(string id)
        {
            Id = id;
            Items = new List<BasketItemDto>();
        }
    }
}
