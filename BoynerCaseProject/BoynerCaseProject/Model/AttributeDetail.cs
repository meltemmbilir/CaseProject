using System.Text.Json.Serialization;

namespace BoynerCaseProject.Model
{
    public class AttributeDetail : Base
    {
        [JsonIgnore]
        public int AttributeId { get; set; }
        public string Name { get; set; }
    }
}
