using BlogApi.Contexts;
using BlogApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly BlogContext _dbContext;
        public PostsController(BlogContext dbContext)
        {
            _dbContext = dbContext;
        }
        // GET: api/<Posts>
        [HttpGet]
        public async Task<IEnumerable<Post>> Get()
        {
            return await _dbContext.Posts.ToListAsync();
        }

        // GET api/<Posts>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> Get(int id)
        {
            Post? post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);

            if (post == null) return NotFound(new { message = "пост не найден" });

            return Ok(post);
        }

        // POST api/<Posts>
        [HttpPost]
        public async Task<ActionResult<Post>> Post([FromBody]Post newPost)
        {
            newPost.CreateAt = DateTime.Now;
            await _dbContext.AddAsync(newPost);
            await _dbContext.SaveChangesAsync();
            return Created();
        }

        // PUT api/<Posts>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Post>> Put(int id, [FromBody]Post post)
        {
            Post? currentPost = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == post.Id);
            if (currentPost == null) return NotFound(new { message = "пост не найден" });
            currentPost.Title = post.Title;
            currentPost.Content = post.Content;
            await _dbContext.SaveChangesAsync();
            return Ok(currentPost);
        }

        // DELETE api/<Posts>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            Post? post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (post == null) return NotFound(new { message = "пост не найден" });
            _dbContext.Remove(post);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
