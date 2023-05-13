namespace Core.Specifications.Order
{
    public class OrderWithItemsAndOrderingSpecification : BaseSpecification<EFModels.Entities.Order.Order>
    {
        public OrderWithItemsAndOrderingSpecification(string email) : base(_ => _.BuyerEmail == email)
        {
            AddInclude(_ => _.Items);
            AddInclude(_ => _.DeliveryMethod);
            AddOrderByDesc(_ => _.OrderDate);
        }

        public OrderWithItemsAndOrderingSpecification(int orderId, string email) : base(_ => _.BuyerEmail == email && _.Id == orderId)
        {
            AddInclude(_ => _.Items);
            AddInclude(_ => _.DeliveryMethod);
        }
    }
}
