using Reddot.Models;

namespace Reddot.Repositories
{
    public interface IPostRepository
    {
        public Task<List<Post>> GetPosts();
        public Task<Post?> GetPost(int id);
        public void AddPost(Post post);
    }
}
