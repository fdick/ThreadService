namespace ThreadService.API.Contracts
{
    public record ThreadsResponse(Guid id,
                                  Guid authorId,
                                  DateTime createdTime,
                                  string header,
                                  string? description,
                                  List<PostResponse> posts
                                  );
}
