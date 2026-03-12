namespace demoWebAPI.API.model.DTO
{
    public class FileUploadDto
    {
        public IFormFile File { get; set; }
        public Guid StateId { get; set; }   
    }
}
