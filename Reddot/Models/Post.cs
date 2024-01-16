using Postgrest.Attributes;
using Postgrest.Models;

namespace Reddot.Models
{
    [Table("posts")]
    public class Post : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("author")]
        public string? Author { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("score")]
        public int Score { get; set; }

        [Column("title")]
        public string? Title { get; set; }

        [Column("content")]
        public string? Content { get; set; }
    }
}
