namespace Reddot.Models;

public class UserVoteDTO
{
    public int Id { get; set; }
    
    public PostDTO Post { get; set; }

    public string UserUID { get; set; }
    
    public bool IsUpvote { get; set; }

    public static UserVoteDTO fromUserVote(UserVote userVote)
    {
        return new UserVoteDTO()
        {
            Id = userVote.Id,
            IsUpvote = userVote.IsUpvote,
            UserUID = userVote.UserUID,
            Post = PostDTO.fromPost(userVote.Post),
        };
    }
}