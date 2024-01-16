using Postgrest;
using Reddot.Models;

namespace Reddot.Repositories;

public class UserVoteRepository(Supabase.Client client) : IUserVoteRepository
{
    public async Task<UserVote?> GetUserVote(string userUid, int postId)
    {
        return await client.From<UserVote>()
            .Where(vote => vote.UserUID == userUid && vote.Post.Id == postId)
            .Single();
    }

    public async Task<int> GetPostScore(int postId)
    {
        var upvotes = await client.From<UserVote>()
            .Where(vote => vote.Post.Id == postId && vote.IsUpvote)
            .Count(Constants.CountType.Exact);
        var downvotes = await client.From<UserVote>()
            .Where(vote => vote.Post.Id == postId && !vote.IsUpvote)
            .Count(Constants.CountType.Exact);
        return upvotes - downvotes;
    }
    
    public async void AddVote(UserVote vote)
    {
        var previousVote = await GetUserVote(vote.UserUID, vote.Post.Id);
        if (previousVote != null)
        {
            // If already voted, update vote
            await client.From<UserVote>()
                .Where(x => x.Id == previousVote.Id)
                .Set(x => x.IsUpvote, vote.IsUpvote)
                .Update();
        }
        else
        {
            await client.From<UserVote>().Insert(vote);
        }
    }
    
    public async void CancelVote(int voteId)
    {
        await client.From<UserVote>()
            .Where(vote => vote.Id == voteId)
            .Delete();
    }
}