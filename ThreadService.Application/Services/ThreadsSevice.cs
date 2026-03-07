using ThreadService.Core.Abstractions;

namespace ThreadService.Application.Services
{
    public class ThreadsSevice : IThreadsSevice
    {
        private readonly IThreadsRepository _repository;

        public ThreadsSevice(IThreadsRepository repository)
        {
            this._repository = repository;
        }

        public async Task<List<(Core.Models.Thread, string)>> GetAllAsync()
        {
            return await _repository.GetAll();
        }

        public async Task<(Core.Models.Thread, string)> GetThreadWithAllPosts(Guid threadId)
        {
            return await _repository.GetThreadWithPosts(threadId);
        }

        public async Task<Guid> CreateAsync(Core.Models.Thread thread)
        {
            return await _repository.CreateThread(thread);
        }

        public async Task<Guid> DeleteAsync(Guid threadId)
        {
            return await _repository.DeleteThread(threadId);
        }

        public async Task<Guid> UpdateAsync(Core.Models.Thread updatedThread)
        {
            return await _repository.UpdateThread(updatedThread);
        }
    }
}
