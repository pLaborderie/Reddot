using Reddot.Models;

namespace Reddot.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly Supabase.Client _client;
        public PostRepository(Supabase.Client client)
        {
            _client = client;
        }
        public async Task<List<Post>> GetPosts()
        {
            var result = await _client.From<Post>().Get();
            return result.Models;
        }

        public async Task<Post?> GetPost(int id)
        {
            var result = await _client.From<Post>()
                .Where(x => x.Id == id)
                .Single();
            return result;
        }

        public async void AddPost(Post post)
        {
            await _client.From<Post>().Insert(post);
        }
    }
}
