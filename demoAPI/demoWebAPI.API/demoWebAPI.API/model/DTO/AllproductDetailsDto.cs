namespace demoWebAPI.API.model.DTO
{
    public class AllproductDetailsDto
    {
        public Guid ProductFileId { get; set; }
        public string FileName { get; set; }
       
        public string FileUrl { get; set; }
        

        // Product details
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int ProductPrice { get; set; }

        // Category details
        public Guid CategoryId { get; set; }
        public CategoryDto Category { get; set; }

    }
}
