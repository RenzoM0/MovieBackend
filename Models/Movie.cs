using System.ComponentModel.DataAnnotations;

namespace MovieBackend.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }

        [Range(0,9999)]
        public int Year { get; set; }

        // Movie heeft 1 director verwijzing
        public int DirectorId { get; set; }
        public Director Director { get; set; }
        
        // 0 of meerdere reviews
        public List<Review> Reviews { get; set; } = new List<Review>();
    }
}
