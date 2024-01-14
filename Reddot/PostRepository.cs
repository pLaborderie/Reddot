namespace Reddot
{
    public class PostRepository : IPostRepository
    {
        public List<Post> GetPosts()
        {
            using var context = new ApiContext();
            return [.. context.Posts];
    }

        public Post? GetPost(int id)
        {
            using var context = new ApiContext();
            return context.Posts.Find(id);
        }

        public void AddPost(Post post)
        {
            using var context = new ApiContext();
            context.Posts.Add(post);
            context.SaveChanges();
        }
    }
}
