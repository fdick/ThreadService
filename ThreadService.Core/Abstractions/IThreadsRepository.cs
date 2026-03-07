

namespace ThreadService.Core.Abstractions
{
    public interface IThreadsRepository
    {
        Task<Guid> CreateThread(Core.Models.Thread thread);
        Task<Guid> DeleteThread(Guid threadId);
        Task<List<(Core.Models.Thread, string)>> GetAll();
        Task<(Core.Models.Thread, string)> GetThreadWithPosts(Guid threadId);
        Task<Guid> UpdateThread(Core.Models.Thread updatedThread);
    }
}