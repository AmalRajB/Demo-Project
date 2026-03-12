namespace demoWebAPI.API.model.DTO
{
    public class FileUploadResponseDto
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string FileExtension { get; set; }

        public long FileSize { get; set; }

        public string FileUrl { get; set; }

        public DateTime CreatedDate { get; set; }
        public Guid StateId { get; set; }
    }
}
