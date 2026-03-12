namespace demoWebAPI.API.model.domain
{
    public class productCategory
    {
        public Guid id { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Products> products { get; set; }
    }
}
