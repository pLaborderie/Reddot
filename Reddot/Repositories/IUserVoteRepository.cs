using Reddot.Models;

namespace Reddot.Repositories;

public interface IUserVoteRepository
{
    public Task<UserVote?> GetUserVote(string userUid, int postId);
    public Task<int> GetPostScore(int postId);
    public void AddVote(UserVote vote);
    public void CancelVote(int voteId);

}