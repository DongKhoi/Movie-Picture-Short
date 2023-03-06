using Application.Interfaces;
using Domain.Common;
using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Movie_Web_API.Controllers
{
    [Route("api/reaction")]
    [ApiController]
    public class ReactionController : ControllerBase
    {
        private readonly IReactionMovieService _reactionService;

        public ReactionController(IReactionMovieService reactionService)
        {
            _reactionService = reactionService;
        }

        [HttpPost("check")]
        public async Task<ActionResult<ReactionMovie>> CheckReactionMovieAsync([FromBody] ReactionMovieDTO reactionDTO)
        {
            return await _reactionService.GetDetail(reactionDTO);
        }

        [HttpPost]
        public async Task<ActionResult<Response<Guid>>> CreateReactionMovieAsync([FromBody] ReactionMovieDTO reactionDTO)
        {
            return await _reactionService.Create(reactionDTO);
        }

        [HttpPost("remove")]
        public async Task<ActionResult<Response<Guid>>> RemoveReactionMovieAsync([FromBody] ReactionMovieDTO reactionDTO)
        {
            return await _reactionService.Remove(reactionDTO);
        }
    }
}
