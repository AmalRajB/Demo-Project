namespace demoWebAPI.API.model.DTO
{
    public class EditProductImageDto
    {
        public IFormFile File { get; set; }
        public Guid ProductId { get; set; }
    }
}
