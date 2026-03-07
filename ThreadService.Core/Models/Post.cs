namespace ThreadService.Core.Models
{
    public class Post
    {
        private Post(
            Guid iD,
            string message,
            int likeQuantity,
            int dislikeQuantity,
            DateTime createTime,
            Guid? parentMessageID,
            Guid userID,
            Guid threadID)
        {
            ID = iD;
            Message = message;
            LikeQuantity = likeQuantity;
            DislikeQuantity = dislikeQuantity;
            CreateTime = createTime;
            ParentPostID = parentMessageID;
            UserID = userID;
            ThreadID = threadID;
        }

        public Guid ID { get; }
        public string Message { get; }
        public int LikeQuantity { get; }
        public int DislikeQuantity { get; }
        public DateTime CreateTime { get; }
        public Guid? ParentPostID { get; }
        public Guid UserID { get; }
        public Guid ThreadID { get; }

        public static (Post, string) Create(Guid id, string msg, int likeQuantity, int dislikeQuantity, DateTime createTime, Guid? parentMessageID, Guid userID, Guid threadID)
        {
            var error = string.Empty;

            //validation
            //...

            var post = new Post(id, msg, likeQuantity, dislikeQuantity, createTime, parentMessageID, userID, threadID);

            return (post, error);
        }
    }
}
