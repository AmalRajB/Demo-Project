namespace demoWebAPI.API.model.DTO
{
    public class AddproductDto
    {
        public string productName { get; set; }
        public int productPrice { get; set; }
        public Guid categoryId { get; set; }

    }
}
