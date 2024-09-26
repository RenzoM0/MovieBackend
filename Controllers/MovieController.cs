using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBackend.DbContexts;
using MovieBackend.DTO;
using MovieBackend.Models;

namespace MovieBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }
            var movieDTO = new MovieDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                DirectorId = movie.DirectorId
            };
            return Ok(movieDTO);
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

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movieDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AlterMovie(int id, MovieDTO movieDTO)
        {
            if (id != movieDTO.Id)
            {
                return BadRequest();
            }

            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.SingleOrDefaultAsync(m =>m.Id == id);
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