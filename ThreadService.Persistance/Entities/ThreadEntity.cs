namespace ThreadService.Persistance.Entities
{
    public class ThreadEntity
    {
        public Guid ID { get; set; }
        public Guid AuthorID { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Header { get; set; }
        public string? Description { get; set; }

    }
}
