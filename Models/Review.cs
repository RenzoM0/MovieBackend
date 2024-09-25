using System.ComponentModel.DataAnnotations;

namespace MovieBackend.Models
{
    public class Review
    {
        public int Id { get; set; }
        
        [Range(1, 10)]
        public int Rating { get; set; }
       
        [MaxLength(1000)]
        public string Description { get; set; }
        
        [MaxLength(50)]
        public string UserName { get; set; }
        
        public DateTime CreatedAt { get; set; }

        // Review heeft 1 movie verwijzing
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
    }
}
