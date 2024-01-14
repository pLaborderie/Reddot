using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Reddot.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostController : Controller
    {
        readonly IPostRepository _postRepository;
        public PostController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        public List<Post> GetPosts()
        {
            return _postRepository.GetPosts();
        }

        [HttpGet("{id}")]
        public Post? GetPost([FromRoute] int id)
        {
            return _postRepository.GetPost(id);
        }

        [HttpPost]
        public Post? CreatePost(Post post)
        {
            post.Id = 0; // Ignore Id sent from client, generated on insert with autoincrement
            _postRepository.AddPost(post);
            return post;
        }
    }
}
