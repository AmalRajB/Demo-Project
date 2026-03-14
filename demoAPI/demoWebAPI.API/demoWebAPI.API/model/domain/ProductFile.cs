namespace demoWebAPI.API.model.domain
{
    public class ProductFile
    {
        public Guid id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

        public string FileExtension { get; set; }

        public long FileSize { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Guid ProductId { get; set; }
        public Products Product { get; set; }
    }
}
