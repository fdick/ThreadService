using System;
using System.Collections.Generic;
using System.Text;

namespace ThreadService.DataAccess.Entities
{
    public class PostEntity
    {
        public Guid ID { get; set; }
        public string Message { get; set; } = string.Empty;
        public int LikeQuantity { get; set; }
        public int DislikeQuantity { get; set; }
        public DateTime CreateTime { get; set; }
        public Guid? ParentMessageID { get; set; }

        public Guid UserID { get; set; }
        //public UserEntity User { get; set; }

        public Guid ThreadID { get; set; }
        public ThreadEntity Thread { get; set; }
    }
}
