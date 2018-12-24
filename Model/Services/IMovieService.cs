using Model.DomainModel;
using System.Collections.Generic;
using WebApi.DTO;

namespace Model.Services
{
    public interface IMovieService
    {
        Movie GetOne(int id);
        IEnumerable<Movie> GetAll();

        IEnumerable<Movie> GetTopMoviesByRateValue(int count);
        IEnumerable<Movie> GetTopMoviesByRateCount(int count);
        IEnumerable<Movie> GetTopRatedMoviesOfUser(int userId, int limit);
        IEnumerable<Movie> Find(MovieSearchCriteria searchCriteria, int limit);

        void RateMovie(int movieId, int userId, int rate);
    }



}
