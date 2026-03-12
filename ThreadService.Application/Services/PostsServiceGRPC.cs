using Grpc.Core;
using PostService.API.GRPC.Protos;
using ThreadService.Application.Abstractions;

namespace ThreadService.Application.Services
{
    public class PostsServiceGRPC : IPostsServiceGRPC
    {
        private readonly GRPCPostsController.GRPCPostsControllerClient _client;

        public PostsServiceGRPC(GRPCPostsController.GRPCPostsControllerClient client)
        {
            this._client = client;
        }

        public async Task<GRPCPostResponse> GetPostsAsync(GRPCPostRequest request, string accessToken = null, CancellationToken cancellationToken = default)
        {
            var metadata = new Metadata()
            {
                {"Authorization", $"Bearer {accessToken}"}
            };

            return await _client.GetPostsAsync(request, metadata, null, cancellationToken);
        }

    }
}
