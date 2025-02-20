namespace GitHub_Repository_Viewer.Models
{
    public class Repository
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Stars { get; set; } // Maps to "stargazers_count" in the API
        public int Forks { get; set; } // Maps to "forks_count" in the API
    }
}
