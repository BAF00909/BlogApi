using BlogApi.Interfaces;
using BlogApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogApi.Controllers
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
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _postService.GetAllAsync();
            return Ok(posts);
        }

        // GET api/<Posts>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Post? post = await _postService.GetByIdAsync(id);
            if (post == null) return NotFound(new { message = "пост не найден" });
            return Ok(post);
        }

        // POST api/<Posts>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]PostDto newPost)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var post = await _postService.CreateAsync(newPost);
            return CreatedAtAction(nameof(GetById), new {id = post.Id}, post);
        }

        // PUT api/<Posts>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> Put(int id, [FromBody]PostDto post)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            try
            {
                await _postService.UpdateAsync(id, post);
                return NoContent();
            } catch(KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE api/<Posts>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _postService.DeleteAsync(id);
                return NoContent();
            } catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
