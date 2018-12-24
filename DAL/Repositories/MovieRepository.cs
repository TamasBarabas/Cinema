using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Model.Data;
using Model.DomainModel;

namespace DAL.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(CinemaContext context) 
            : base(context)
        { }

        public override Movie GetOne(int id)
        {
            Movie entity = CinemaContext.Movies.Include(u => u.Ratings).SingleOrDefault(u => u.MovieId == id);
            if (entity == null) throw new KeyNotFoundException($"Movie with id '{id}' cannot be found");
            return entity;
        }

        public override Movie GetOneOrDefault(int id)
        {
            return CinemaContext.Movies.Include(u => u.Ratings).SingleOrDefault(u => u.MovieId == id);
        }

        public override IEnumerable<Movie> GetAll()
        {
            return CinemaContext.Movies.Include(u => u.Ratings).ToList();
        }

        public IEnumerable<Movie> GetTopMoviesByAverageRating(int count=5)
        {
            return CinemaContext.Movies
                .Include(u => u.Ratings).ToList()
                .OrderByDescending(m => m.AverageRating)
                    .ThenBy(m => m.Title)
                .Take(count);

        }

        public IEnumerable<Movie> GetTopMoviesByRateCount(int count=5)
        {
            return CinemaContext.Movies
                .Include(u => u.Ratings).ToList()
                .OrderByDescending(m => m.RatingCount)
                    .ThenBy(m => m.Title)
                .Take(count);
        }

        public CinemaContext CinemaContext
        {
            get { return context as CinemaContext; }
        }
    }
}
