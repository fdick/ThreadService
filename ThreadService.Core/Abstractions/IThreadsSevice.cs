



namespace ThreadService.Core.Abstractions
{
    public interface IThreadsSevice
    {
        Task<Guid> CreateAsync(Core.Models.Thread thread);
        Task<Guid> DeleteAsync(Guid threadId);
        Task<List<(Core.Models.Thread, string)>> GetAllAsync();
        Task<(Models.Thread, string)> GetThreadWithAllPosts(Guid threadId);
        Task<Guid> UpdateAsync(Core.Models.Thread updatedThread);
    }
}