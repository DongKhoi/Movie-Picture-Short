using Application.Interfaces;
using Domain.Common;
using Domain.DTOs;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<int>> GetAllMovieAsync([FromRoute] Guid id)
        {
            return Ok(await _reactionService.GetDetail(id));
        }

        [HttpPost]
        public async Task<ActionResult<Response<Guid>>> CreateMovieAsync([FromBody] ReactionMovieDTO reactionDTO)
        {
            return await _reactionService.Create(reactionDTO);
        }
    }
}
