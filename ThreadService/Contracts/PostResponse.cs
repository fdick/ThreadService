namespace ThreadService.API.Contracts
{
    public record PostResponse(
            Guid iD,
            string message,
            int likeQuantity,
            int dislikeQuantity,
            DateTime createTime,
            Guid? parentMessageID,
            Guid userID,
            Guid threadID
        );
}
