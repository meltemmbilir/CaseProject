using System.Text.Json.Serialization;

namespace BoynerCaseProject.Model
{
    public class Category : Base
    {
        public string Name { get; set; }
        public List<Attribute> CategoryAttributes { get; set; }
    }
}
