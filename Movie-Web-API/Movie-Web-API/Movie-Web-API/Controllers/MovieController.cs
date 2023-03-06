using Domain.Common;
using Domain.DTOs;
using Domain.Entities;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Movie_Web_API.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult<Movie?>> GetMovieAsync()
        {
            return Ok(await _movieService.GetMovie());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie?>> GetMovieDetailAsync([FromRoute] Guid id)
        {
            return Ok(await _movieService.GetDetailMovie(id));
        }

        [HttpPost]
        public async Task<ActionResult<Response<Guid>>> CreateMovieAsync([FromBody] MovieDTO movieDTO)
        {
            return await _movieService.Create(movieDTO);
        }
    }
}
