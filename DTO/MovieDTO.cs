using System.ComponentModel.DataAnnotations;

namespace MovieBackend.DTO
{
    public class MovieDTO
    {
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "Titel mag maximaal 100 tekens bevatten.")]
        public string Title { get; set; }

        [Range(0, 9999, ErrorMessage = "Het jaar moet tussen 0 en 9999 liggen.")]
        public int Year { get; set; }

        public int DirectorId { get; set; }
    }
}
