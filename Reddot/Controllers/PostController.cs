using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reddot.Models;
using Reddot.Repositories;

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
        public List<PostDTO> GetPosts()
        {
            var result = _postRepository.GetPosts().Result;
            var postDtos = new List<PostDTO>();
            foreach (var model in result)
            {
                postDtos.Add(new PostDTO()
                {
                    Id = model.Id,
                    Title = model.Title,
                    Content = model.Content,
                    Author = model.Author,
                    Score = model.Score,
                    Date = model.Date,
                });
            }
            System.Diagnostics.Debug.WriteLine(postDtos);
            return postDtos;
        }

        [HttpGet("{id}")]
        public PostDTO? GetPost([FromRoute] int id)
        {
            var result = _postRepository.GetPost(id).Result;
            return new PostDTO()
            {
                Id = result.Id,
                Title = result.Title,
                Content = result.Content,
                Author = result.Author,
                Score = result.Score,
                Date = result.Date,
            };
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
            _postRepository.AddPost(post);
            return postDTO;
        }
    }
}
