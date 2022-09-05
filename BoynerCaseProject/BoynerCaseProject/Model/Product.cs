namespace BoynerCaseProject.Model
{
    public class Product : Base
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public long Price { get; set; }
        public List<Attribute> Attribute { get; set; }
    }
}
