namespace Core.Specifications.Product
{
    public class ProductSpecParams
    {
        public const int MaxPageSize = 50;
        public const int MinPageSize = 5;

        public string? Sort { get;set; }
        public int PageIndex {get; set;} = 1;

        private int _pageSize = MinPageSize;
        public int PageSize {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize 
                ? MaxPageSize
                : value < MinPageSize
                    ? MinPageSize
                    : value;
        }

        public int? BrandId { get;set; }
        public int? TypeId { get;set; }
        public string? Search { get; set; }
    }
}