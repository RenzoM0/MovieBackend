using System.ComponentModel.DataAnnotations;

namespace MovieBackend.DTO
{
    public class ReviewDTO
    {
        public int Id { get; set; }

        [Range(1, 10, ErrorMessage = "Beoordeling moet een waarde hebben tussen 1 en 10.")]
        public int Rating { get; set; }

        [MaxLength(50, ErrorMessage = "Gebruikersnaam mag maximaal 50 tekens bevatten.")]
        public string UserName { get; set; }

        [MaxLength(1000, ErrorMessage = "Beschrijving mag maximaal 1000 tekens bevatten.")]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public int MovieId { get; set; }
    }
}
