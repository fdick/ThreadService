using Microsoft.EntityFrameworkCore;
using PostService.API.GRPC.Protos;
using ThreadService.Core.Abstractions;
using ThreadService.Core.Models;
using ThreadService.DataAccess.Entities;

namespace ThreadService.DataAccess.Repositories
{
    public class ThreadsRepository : IThreadsRepository
    {
        private readonly ThreadServiceDbContext _context;
        private readonly GRPCPostsController.GRPCPostsControllerClient _grpcClient;

        public ThreadsRepository(ThreadServiceDbContext context, GRPCPostsController.GRPCPostsControllerClient grpcClient)
        {
            this._context = context;
            this._grpcClient = grpcClient;
        }

        public async Task<List<(Core.Models.Thread, string)>> GetAll()
        {
            var entities = await _context.Threads.AsNoTracking().ToListAsync();

            var threads = entities.Select(x => Core.Models.Thread.Create(x.ID, x.AuthorID, x.CreatedTime, x.Header, x.Description)).ToList();

            return threads;
        }

        public async Task<(Core.Models.Thread, string)> GetThreadWithPosts(Guid threadId)
        {
            var error = string.Empty;


            //get posts
            GRPCPostRequest req = new GRPCPostRequest() { ThreadID = threadId.ToString() };
            var grpcResponse = _grpcClient.GetPosts(req);

            var posts = grpcResponse.Posts.Select(x => Post.Create(
                Guid.Parse(x.Id),
                x.Content,
                x.LikesQuantity,
                x.DislikesQuantity,
                x.CreatedAt.ToDateTime(),
                x.ParentPostId == null ? null : Guid.Parse(x.ParentPostId),
                Guid.Parse(x.UserId),
                Guid.Parse(x.ThreadId)).Item1
            ).ToList();

            if (grpcResponse == null)
            {
                error = "gRPC response is invalid!";
                return (null, error);
            }

            //get thread
            var entity = _context.Threads.FirstOrDefault(x => x.ID == threadId);

            var (thread, threadError) = Core.Models.Thread.Create(
                entity.ID,
                entity.AuthorID,
                entity.CreatedTime,
                entity.Header,
                entity.Description,
                posts
                );


            return (thread, error);

        }


        public async Task<Guid> CreateThread(Core.Models.Thread thread)
        {
            var entity = new ThreadEntity()
            {
                ID = thread.ID,
                AuthorID = thread.AuthorID,
                Header = thread.Header,
                Description = thread.Description,
                CreatedTime = thread.CreatedTime,
            };

            await _context.Threads.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity.ID;
        }

        public async Task<Guid> DeleteThread(Guid threadId)
        {
            await _context.Threads
                .Where(x => x.ID == threadId)
                .ExecuteDeleteAsync();

            return threadId;
        }

        public async Task<Guid> UpdateThread(Core.Models.Thread updatedThread)
        {
            await _context.Threads
                .Where(x => x.ID == updatedThread.ID)
                .ExecuteUpdateAsync(x => x
                    .SetProperty(r => r.Header, r => updatedThread.Header)
                    .SetProperty(r => r.Description, r => updatedThread.Description)
                    .SetProperty(r => r.CreatedTime, r => updatedThread.CreatedTime)
                    .SetProperty(r => r.AuthorID, r => updatedThread.AuthorID)
                );

            return updatedThread.ID;
        }
    }
}
