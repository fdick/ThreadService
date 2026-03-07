namespace ThreadService.API.Contracts
{
    public record ThreadsRequest(Guid authorId, string header, string? description);
}
