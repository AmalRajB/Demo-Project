namespace demoWebAPI.API.model.DTO
{
    public class ProductDto
    {
        public Guid id { get; set; }
        public string productName { get; set; }
        public int productPrice { get; set; }
        public Guid categoryId { get; set; }
        public CategoryDto category { get; set; }
    }
}
