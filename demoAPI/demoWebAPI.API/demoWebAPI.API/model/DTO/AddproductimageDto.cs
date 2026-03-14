namespace demoWebAPI.API.model.DTO
{
    public class AddproductimageDto
    {
        public IFormFile File { get; set; }
        public Guid ProductId { get; set; }
    }
}
