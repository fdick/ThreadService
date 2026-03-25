using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ThreadService.API.Contracts;
using ThreadService.Core.Abstractions;

namespace ThreadService.API.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    public class ThreadsController : ControllerBase
    {
        private readonly IThreadsSevice _threadsSevice;

        public ThreadsController(IThreadsSevice threadsSevice)
        {
            this._threadsSevice = threadsSevice;
        }

        [HttpGet("GetAll")]
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
        [Route("GetThreadWithPosts")]
        public async Task<ActionResult<List<ThreadsResponse>>> GetThreadWithPosts(Guid threadId)
        {
            var (thread, error) = await _threadsSevice.GetThreadWithAllPosts(threadId);

            if (!string.IsNullOrEmpty(error)) 
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

        [HttpPost("CreateThread")]
        public async Task<ActionResult<Guid>> CreateThread([FromBody] ThreadsRequest request)
        {
            var (thread, error)= Core.Models.Thread.Create(
                Guid.NewGuid(),
                request.authorId,
                DateTime.UtcNow,
                request.header,
                request.description
                );

            if(!string.IsNullOrEmpty(error))
            { 
                return BadRequest(error); 
            }

            var guid = await _threadsSevice.CreateAsync(thread);

            return Ok(guid);
        }

        [HttpDelete("DeleteThread/{threadId:guid}")]
        public async Task<ActionResult<Guid>> DeleteThread(Guid threadId)
        {
            var guid = await _threadsSevice.DeleteAsync(threadId);

            return Ok(guid);
        }

        [HttpPut("UpdateThread")]
        public async Task<ActionResult<Guid>> UpdateThread(Core.Models.Thread updatedThread)
        {
            var guid = await _threadsSevice.UpdateAsync(updatedThread);

            return Ok(guid);
        }

    }
}
