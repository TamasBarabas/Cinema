using Model.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Model.Data
{
    public interface IMovieRepository : IRepository<Movie>
    {
        IEnumerable<Movie> GetTopMoviesByAverageRating(int count=5);
        IEnumerable<Movie> GetTopMoviesByRateCount(int count=5);
    }

}
