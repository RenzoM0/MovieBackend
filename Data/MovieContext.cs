using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieBackend.Models;

namespace MovieBackend.Data
{
    public class MovieContext : IdentityDbContext<User>
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Director> Directors { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Review>()
            .Property(r => r.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
