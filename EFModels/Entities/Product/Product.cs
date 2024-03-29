namespace EFModels.Entities.Product
{
    public class Product : EntityKey
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string PictureUrl { get; set; }
        public ProductType ProductType { get; set; }
        public ProductBrand ProductBrand { get; set; }
        public int ProductTypeId { get; set; }
        public int ProductBrandId { get; set; }
    }
}