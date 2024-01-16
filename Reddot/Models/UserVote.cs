using Postgrest.Attributes;
using Postgrest.Models;

namespace Reddot.Models;

public enum VoteType
{
    Upvote = 1,
    Downvote = -1
}

[Table("user_votes")]
public class UserVote : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    
    [Reference(typeof(Post), ReferenceAttribute.JoinType.Inner, true, "post_id")]
    public Post Post { get; set; }
    
    [Column("post_id")]
    public int PostId { get; set; }

    [Column("user_uid")]
    public string UserUID { get; set; }
    
    [Column("is_upvote")]
    public bool IsUpvote { get; set; }
}