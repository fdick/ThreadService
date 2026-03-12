using PostService.API.GRPC.Protos;

namespace ThreadService.Application.Abstractions
{
    public interface IPostsServiceGRPC
    {
        Task<GRPCPostResponse> GetPostsAsync(GRPCPostRequest request, string accessToken = null, CancellationToken cancellationToken = default);
    }
}