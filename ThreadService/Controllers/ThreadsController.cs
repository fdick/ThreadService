using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThreadService.API.Contracts;
using ThreadService.Core.Abstractions;

namespace ThreadService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ThreadsController : ControllerBase
    {
        private readonly IThreadsSevice _threadsSevice;

        public ThreadsController(IThreadsSevice threadsSevice)
        {
            this._threadsSevice = threadsSevice;
        }

        [HttpGet]
        public async Task<ActionResult<List<ThreadsResponse>>> GetAll()
        {
            var threads = await _threadsSevice.GetAllAsync();

            var response = threads.Select(x => new ThreadsResponse(
                x.Item1.ID,
                x.Item1.AuthorID,
                x.Item1.CreatedTime,
                x.Item1.Header,
                x.Item1.Description,
                new List<PostResponse>()
                )).ToList();

            return Ok(response);
        }

        [HttpGet("{threadId:guid}")]
        public async Task<ActionResult<List<ThreadsResponse>>> GetThreadWithPosts(Guid threadId)
        {
            var (thread, error) = await _threadsSevice.GetThreadWithAllPosts(threadId);

            if (error != null) 
            {
                return BadRequest(error);
            }

            var postsResponse = thread.Posts.Select(x => new PostResponse(x.ID,
                                                                          x.Message,
                                                                          x.LikeQuantity,
                                                                          x.DislikeQuantity,
                                                                          x.CreateTime,
                                                                          x.ParentPostID,
                                                                          x.UserID,
                                                                          x.ThreadID)).ToList();

            var response = new ThreadsResponse(
                thread.ID,
                thread.AuthorID,
                thread.CreatedTime,
                thread.Header,
                thread.Description,
                postsResponse
                );

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateThread([FromBody] ThreadsRequest request)
        {
            var (thread, error)= Core.Models.Thread.Create(
                Guid.NewGuid(),
                request.authorId,
                DateTime.UtcNow,
                request.header,
                request.description
                );

            if(string.IsNullOrEmpty(error))
            { 
                return BadRequest(error); 
            }

            return Ok(thread);
        }

        [HttpDelete("{threadId:guid}")]
        public async Task<ActionResult<Guid>> DeleteThread(Guid threadId)
        {
            var guid = _threadsSevice.DeleteAsync(threadId);

            return Ok(guid);
        }

        [HttpPut]
        public async Task<ActionResult<Guid>> UpdateThread(Core.Models.Thread updatedThread)
        {
            var guid = _threadsSevice.UpdateAsync(updatedThread);

            return Ok(guid);
        }

    }
}
