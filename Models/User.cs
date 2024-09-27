using Microsoft.AspNetCore.Identity;

namespace MovieBackend.Models
{
    public class User : IdentityUser
    {
        public string? FavoriteMovieCharacter { get; set; }
    }
}
