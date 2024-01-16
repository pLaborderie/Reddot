using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reddot.Models;
using Reddot.Repositories;

namespace Reddot.Controllers;

[ApiController]
[Route("user-votes")]
public class UserVoteController(IUserVoteRepository userVoteRepository, IPostRepository postRepository, Supabase.Client client) : Controller
{
    [HttpGet("{userUid}/{postId:int}")]
    public IActionResult GetUserVote([FromRoute] string userUid, [FromRoute] int postId)
    {
        var result = userVoteRepository.GetUserVote(userUid, postId).Result;
        if (result != null)
        {
            return Ok(UserVoteDTO.fromUserVote(result));
        }

        return NotFound();
    }

    [HttpGet("{postId:int}/score")]
    public int GetPostScore([FromRoute] int postId)
    {
        return userVoteRepository.GetPostScore(postId).Result;
    }

    [HttpPost("{postId:int}/vote/{voteType:int}")]
    [Authorize]
    public async Task<IActionResult> Vote([FromRoute] int postId, [FromRoute] VoteType voteType)
    {
        // TODO: Make a middleware to get user
        var authorization = Request.Headers.Authorization;
        // Remove "Bearer " at start of header
        var jwt = authorization.ToString().Remove(0, 7);
        var user = await client.Auth.GetUser(jwt);

        if (user?.Id == null)
        {
            return Unauthorized();
        }

        if (!Enum.IsDefined(typeof(VoteType), voteType))
        {
            return BadRequest("Not a valid vote type.");
        }
        
        var vote = new UserVote()
        {
            IsUpvote = VoteType.Upvote.Equals(voteType),
            UserUID = user.Id,
            PostId = postId,
        };
        var created = await userVoteRepository.AddVote(vote);
        return Ok(UserVoteDTO.fromUserVote(created));
    }

    [HttpDelete("{postId:int}/vote")]
    [Authorize]
    public async Task<IActionResult> CancelVote([FromRoute] int postId)
    {
        var authorization = Request.Headers.Authorization;
        // Remove "Bearer " at start of header
        var jwt = authorization.ToString().Remove(0, 7);
        var user = await client.Auth.GetUser(jwt);
        if (user?.Id == null)
        {
            return Unauthorized();
        }
        
        var vote = await userVoteRepository.GetUserVote(user.Id, postId);
        if (vote == null)
        {
            return NotFound();
        }
        if (vote.UserUID != user.Id)
        {
            return Unauthorized("Cannot delete this vote.");
        }

        await userVoteRepository.CancelVote(vote.Id);
        return NoContent();
    }
}