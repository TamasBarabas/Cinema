using Model.Data;
using Model.DomainModel;
using Model.Specifications;
using Model.Specifications.Factories;
using System.Collections.Generic;
using System.Linq;
using WebApi.DTO;

namespace Model.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repository;
        private readonly IUserService _userService;
        private readonly MovieSpecificationFactory _movieSpecificationFactory;

        public MovieService(IMovieRepository repository, IUserService userService, MovieSpecificationFactory movieSpecificationFactory)
        {
            _repository = repository;
            _userService = userService;
            _movieSpecificationFactory = movieSpecificationFactory;
        }

        public Movie GetOne(int id)
        {
            return _repository.GetOne(id);
        }

        public IEnumerable<Movie> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<Movie> Find(MovieSearchCriteria searchCriteria, int limit)
        {
            IEnumerable<Movie> movies = _repository.Find(
                filter: _movieSpecificationFactory.CreateSpecification(searchCriteria),
                orderBy: r => r.OrderBy(m => m.Title),
                limit: limit
            );

            return movies;
        }

        public IEnumerable<Movie> GetTopRatedMoviesOfUser(int userId, int limit)
        {
            return _repository.Find(
                filter: new RatedByUserSpecification(userId),
                orderBy: r => r.OrderByDescending(m => m.RatingBy(userId)).ThenBy(m => m.Title),
                limit: limit
            );
        }

        public IEnumerable<Movie> GetTopMoviesByRateValue(int limit)
        {
            return _repository.GetTopMoviesByAverageRating(limit);
        }

        public IEnumerable<Movie> GetTopMoviesByRateCount(int limit)
        {
            return _repository.GetTopMoviesByRateCount(limit);
        }

        public void RateMovie(int movieId, int userId, int rate)
        {
            Movie movie = GetOne(movieId);
            User user = _userService.GetOne(userId);
            movie.Rate(user, rate);
            _repository.SaveChanges();
        }

    }


}
