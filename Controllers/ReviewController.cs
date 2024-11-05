using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieBackend.Data;
using MovieBackend.DTO;
using MovieBackend.Models;

namespace MovieBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly MovieContext _context;

        public ReviewController(MovieContext context)
        {
            _context = context;
        }

        // GET: api/Review
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReviewDTO>>> GetReviews()
        {
            return await _context.Reviews.Select(r => new ReviewDTO
            {
                Id = r.Id,
                Rating = r.Rating,
                UserName = r.UserName,
                Description = r.Description,
                MovieId = r.MovieId
            }).ToListAsync();
        }

        // GET: api/Review/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReviewDTO>> GetReview(int id)
        {
            var reviewDTO = await _context.Reviews
                .Where(r => r.Id == id)
                .Select(r => new ReviewDTO
                {
                    Id = r.Id,
                    Rating = r.Rating,
                    UserName = r.UserName,
                    Description = r.Description,
                    MovieId = r.MovieId
                }).SingleOrDefaultAsync();

            if (reviewDTO == null)
            {
                return NotFound();
            }

            return Ok(reviewDTO);
        }

        // PUT: api/Review/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReview(int id, ReviewDTO reviewDTO)
        {
            if (id != reviewDTO.Id)
            {
                return BadRequest();
            }

            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                NotFound();
            }

            review.Rating = reviewDTO.Rating;
            review.UserName = reviewDTO.UserName;
            review.Description = reviewDTO.Description;
            review.MovieId = reviewDTO.MovieId;

            _context.Entry(review).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!ReviewExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Review
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ReviewDTO>> PostReview(ReviewDTO reviewDTO)
        {
            var review = new Review
            {
                Rating = reviewDTO.Rating,
                UserName = reviewDTO.UserName,
                Description = reviewDTO.Description,
                MovieId = reviewDTO.MovieId
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReview), new { id = review.Id }, reviewDTO);
        }

        // DELETE: api/Review/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReviewExists(int id)
        {
            return _context.Reviews.Any(e => e.Id == id);
        }
    }
}
