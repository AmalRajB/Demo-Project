using System.ComponentModel.DataAnnotations;

namespace demoWebAPI.API.model.domain
{
    public class FileModel
    {
        [Key]
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string FileExtension { get; set; }

        public long FileSize { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
   

        public Guid StateId { get; set; }
        public State State { get; set; }
    }
}
