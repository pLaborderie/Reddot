using Reddot.Models;

namespace Reddot.Repositories
{
    public class PostRepository(Supabase.Client client) : IPostRepository
    {
        public async Task<List<Post>> GetPosts()
        {
            var result = await client.From<Post>().Get();
            return result.Models;
        }

        public async Task<Post?> GetPost(int id)
        {
            var result = await client.From<Post>()
                .Where(x => x.Id == id)
                .Single();
            return result;
        }

        public async void AddPost(Post post)
        {
            await client.From<Post>().Insert(post);
        }
    }
}
