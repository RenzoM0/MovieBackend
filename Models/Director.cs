using System.ComponentModel.DataAnnotations;

namespace MovieBackend.Models
{
    public class Director
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public List<Movie> Movies { get; set; } = new List<Movie>(); // Initialisatie
    }
}
