namespace Api.Model
{
    public class RepositoryData
    {
        public RepositoryData(string name)
        {
            this.name = name;
        }

        private string Id { get; set; }

        public string name { get; }

        public override string ToString()
        {
            return "name=" + name + ", Id=" + Id;
        }
    }
}