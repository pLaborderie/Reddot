using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reddot.Models;
using Reddot.Repositories;

namespace Reddot.Controllers
{
    [ApiController]
    [Route("posts")]
    public class PostController(IPostRepository postRepository) : Controller
    {
        [HttpGet]
        public List<PostDTO> GetPosts()
        {
            var result = postRepository.GetPosts().Result;
            var postDtos = new List<PostDTO>();
            foreach (var model in result)
            {
                postDtos.Add(PostDTO.fromPost(model));
            }
            System.Diagnostics.Debug.WriteLine(postDtos);
            return postDtos;
        }

        [HttpGet("{id}")]
        public PostDTO? GetPost([FromRoute] int id)
        {
            var result = postRepository.GetPost(id).Result;
            return PostDTO.fromPost(result);
        }

        [HttpPost]
        public PostDTO? CreatePost(PostDTO postDTO)
        {
            Post post = new Post()
            {
                Author = postDTO.Author,
                Title = postDTO.Title,
                Content = postDTO.Content,
            };
            postRepository.AddPost(post);
            return postDTO;
        }
    }
}
