using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBackend.Data;
using MovieBackend.DTO;
using MovieBackend.Models;

namespace MovieBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MovieController : ControllerBase
    {
        private readonly MovieContext _context;

        public MovieController(MovieContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieDTO>>> GetMovies()
        {
            return await _context.Movies.Select(m => new MovieDTO
            {
                Id = m.Id,
                Title = m.Title,
                Year = m.Year,
                DirectorId = m.DirectorId
            }).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovieDTO>> GetMovie(int id)
        {
            var movieDTO = await _context.Movies
                .Where(m => m.Id == id)
                .Select(m => new MovieDTO
                {
                    Id = m.Id,
                    Title = m.Title,
                    Year = m.Year,
                    DirectorId = m.DirectorId
                }).SingleOrDefaultAsync();


            if (movieDTO == null)
            {
                return NotFound();
            }

            return Ok(movieDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutMovie(int id, MovieDTO movieDTO)
        {
            if (id != movieDTO.Id)
            {
                return BadRequest();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            movie.Title = movieDTO.Title;
            movie.Year = movieDTO.Year;
            movie.DirectorId = movieDTO.DirectorId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!MovieExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<MovieDTO>> PostMovie(MovieDTO movieDTO)
        {
            var movie = new Movie
            {
                Title = movieDTO.Title,
                Year = movieDTO.Year,
                DirectorId = movieDTO.DirectorId
            };

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            movieDTO.Id = movie.Id; // Set de Id in de DTO

            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movieDTO);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}