namespace Reddot
{
    public class Post
    {
        public int Id { get; set; }

        public required string Author { get; set; }

        public DateTime Date { get; set; }

        public int Score { get; set; }

        public required string Title { get; set; }

        public string? Content { get; set; }
    }
}
