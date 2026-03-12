using System.ComponentModel.DataAnnotations.Schema;

namespace demoWebAPI.API.model.domain
{
    public class Products
    {

        public Guid id { get; set; }
        public  string productName { get; set; }
        public int productPrice { get; set; }
        public Guid categoryId { get; set; }

        [ForeignKey("categoryId")]
        public productCategory category { get; set; }

    }
}
