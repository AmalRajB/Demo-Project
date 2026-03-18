namespace demoWebAPI.API.model.DTO
{
    public class AllproductDetailsDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }
        public CategoryDto Category { get; set; }
        public List<ProductImageDto> ProductFiles { get; set; }

    }
}
