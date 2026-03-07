namespace ThreadService.Core.Models
{
    public class Thread
    {
        private Thread(Guid iD, Guid authorID, DateTime createdTime, string header, string? description, List<Post>? posts = default)
        {
            ID = iD;
            AuthorID = authorID;
            CreatedTime = createdTime;
            Header = header;
            Description = description;
            Posts = posts;

            if(posts == null)
                Posts = new List<Post>();
        }

        public Guid ID { get; }
        public Guid AuthorID { get; }
        public DateTime CreatedTime { get; }
        public string Header { get;  }
        public string? Description { get;  }
        public List<Post> Posts { get; } = new List<Post>();

        public static (Thread, string) Create(Guid id, Guid authorID, DateTime createdTime, string header, string? description, List<Post> posts = default)
        {
            var error = string.Empty;

            //validate data
            //...

            var t =  new Thread(id, authorID, createdTime, header, description, posts);

            return (t, error);
        }
    }
}
