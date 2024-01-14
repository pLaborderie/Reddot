namespace Reddot
{
    public interface IPostRepository
    {
        public List<Post> GetPosts();
        public Post? GetPost(int id);
        public void AddPost(Post post);
    }
}
