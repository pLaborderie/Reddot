using Postgrest.Attributes;
using Postgrest.Models;

namespace Reddot.Models;

[Table("user_votes")]
public class UserVote : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    
    [Reference(typeof(Post))]
    public Post Post { get; set; }

    [Column("user_uid")]
    public string UserUID { get; set; }
    
    [Column("is_upvote")]
    public bool IsUpvote { get; set; }
}