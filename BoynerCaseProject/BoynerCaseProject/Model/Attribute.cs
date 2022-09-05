using System.Text.Json.Serialization;

namespace BoynerCaseProject.Model
{
    public class Attribute : Base
    {
        [JsonIgnore]
        public int CategoryId { get; set; }
        public string Name { get; set; } //size , color 
        public List<AttributeDetail> AttributeDetail { get; set; } // xl , l , red 
    }
}
