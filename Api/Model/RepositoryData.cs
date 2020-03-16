using Newtonsoft.Json;

namespace Api.Model
{
    public class RepositoryData
    {
        public RepositoryData(string name)
        {
            Name = name;
        }

        private string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; }

        public override string ToString()
        {
            return "name=" + Name + ", Id=" + Id;
        }
    }
}