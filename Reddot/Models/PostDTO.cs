namespace Reddot.Models
{
    public class PostDTO
    {
        public int Id { get; set; }

        public string? Author { get; set; }

        public DateTime Date { get; set; }

        public int Score { get; set; }

        public string? Title { get; set; }

        public string? Content { get; set; }

        public static PostDTO fromPost(Post post)
        {
            return new PostDTO()
            {
                Id = post.Id,
                Author = post.Author,
                Date = post.Date,
                Score = post.Score,
                Title = post.Title,
                Content = post.Content
            };
        }
    }
}
