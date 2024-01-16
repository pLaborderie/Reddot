namespace Reddot.Models;

public class UserVoteDTO
{
    public int Id { get; set; }
    
    public Post Post { get; set; }

    public string UserUID { get; set; }
    
    public bool IsUpvote { get; set; }
}