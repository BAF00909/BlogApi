using BlogApi.Application.Dtos;
using BlogApi.Application.Interfaces;
using BlogApi.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }
        // GET: api/<Posts>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? title = null,
            [FromQuery] string? sortBy = null,
            [FromQuery] string? order = null
            )
        {
            var (posts, totalCount) = await _postService.GetAllAsync(page, pageSize, title, sortBy, order);
            return Ok(new
            {
                Posts = posts,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize
            });
        }

        // GET api/<Posts>/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Post? post = await _postService.GetByIdAsync(id);
            if (post == null) return NotFound(new { message = "пост не найден" });
            return Ok(post);
        }

        // POST api/<Posts>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PostDto newPost)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var post = await _postService.CreateAsync(newPost);
            return CreatedAtAction(nameof(GetById), new {id = post.Id}, post);
        }

        // PUT api/<Posts>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> Put(int id, [FromBody]PostDto post)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var update = await _postService.UpdateAsync(id, post);
            if (!update) return NotFound();
            return NoContent();
        }

        // DELETE api/<Posts>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var delete = await _postService.DeleteAsync(id);
            if (!delete) return NotFound();
            return NoContent();
        }
    }
}
