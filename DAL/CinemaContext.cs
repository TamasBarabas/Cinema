using Microsoft.EntityFrameworkCore;
using Model.DomainModel;

namespace DAL
{
    public class CinemaContext : DbContext
    {
        public CinemaContext (DbContextOptions<CinemaContext> options)
            : base(options)
        { }

        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(u => new { u.UserId });
            modelBuilder.Entity<User>()
                .Property(f => f.UserId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Movie>()
                .HasKey(m => new { m.MovieId });
            modelBuilder.Entity<Movie>()
                .Property(f => f.MovieId).ValueGeneratedOnAdd();

            modelBuilder.Entity<Rating>()
                .HasKey(r => new { r.UserId, r.MovieId});


            modelBuilder.Seed();
        }
    }
}
