using Postgrest;
using Reddot.Models;

namespace Reddot.Repositories;

public class UserVoteRepository(Supabase.Client client) : IUserVoteRepository
{
    public async Task<UserVote?> GetUserVote(string userUid, int postId)
    {
        return await client.From<UserVote>()
            .Where(vote => vote.UserUID == userUid && vote.PostId == postId)
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
    
    public async Task<UserVote?> AddVote(UserVote vote)
    {
        var previousVote = await GetUserVote(vote.UserUID, vote.PostId);
        if (previousVote != null)
        {
            // If already voted, update vote
            return (await client.From<UserVote>()
                .Where(x => x.Id == previousVote.Id)
                .Set(x => x.IsUpvote, vote.IsUpvote)
                .Update()).Model;
        }
        else
        {
            return (await client.From<UserVote>().Insert(vote)).Model;
        }
    }
    
    public Task CancelVote(int voteId)
    {
        return client.From<UserVote>()
            .Where(vote => vote.Id == voteId)
            .Delete();
    }
}