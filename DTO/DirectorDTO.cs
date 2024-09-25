using System.ComponentModel.DataAnnotations;

namespace MovieBackend.DTO
{
    public class DirectorDTO
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "Naam mag maximaal 50 tekens bevatten.")]
        public string Name { get; set; }
    }
}
