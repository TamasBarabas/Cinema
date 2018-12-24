using Microsoft.AspNetCore.Mvc;
using Model.Data;
using Model.DomainModel;
using Model.Services;
using Model.Specifications;
using Model.Specifications.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using WebApi.DTO;
using WebApi.Helpers;
using WebApi.ViewModels;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly RoundRate _roundHelper;

        public MoviesController(IMovieService movieService, RoundRate roundHelper)
        {
            _movieService = movieService;
            _roundHelper = roundHelper;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<MovieViewModel> Get(int id)
        {
            Movie movie = _movieService.GetOne(id);

            if (movie == null) return NotFound("No movie found");

            return viewModel(movie);
        }

        [HttpGet]
        public ActionResult<IEnumerable<MovieViewModel>> Get()
        {
            IList<Movie> movies = _movieService.GetAll().ToList();

            if (!movies.Any()) return NotFound("No movie found");

            return viewModel(movies);
        }

        [HttpGet("[action]")]
        public ActionResult<IEnumerable<MovieViewModel>> Find([FromQuery]MovieSearchCriteria searchCriteria, int limit = 5)
        {
            IList<Movie> movies = _movieService.Find(searchCriteria, limit).ToList();

            if (!movies.Any()) return NotFound("No movie found");

            return viewModel(movies);
        }


        [HttpGet("[action]/{limit}")]
        public ActionResult<IEnumerable<MovieViewModel>> GetTopMoviesByRateValue(int limit = 5)
        {
            IList<Movie> movies = _movieService.GetTopMoviesByRateValue(limit).ToList();

            if (!movies.Any()) return NotFound("No movie found");

            return viewModel(movies);
        }

        [HttpGet("[action]/{limit}")]
        public ActionResult<IEnumerable<MovieViewModel>> GetTopMoviesByRateCount(int limit = 5)
        {
            IList<Movie> movies = _movieService.GetTopMoviesByRateCount(limit).ToList();

            if (!movies.Any()) return NotFound("No movie found");

            return viewModel(movies);
        }

        [HttpGet("[action]/{userId}/{limit}")]
        public ActionResult<IEnumerable<MovieViewModel>> GetTopRatedMoviesOfUser(int userId, int limit = 5)
        {
            IList<Movie> movies = _movieService.GetTopRatedMoviesOfUser(userId, limit).ToList();

            if (!movies.Any()) return NotFound("No movie found");

            return viewModel(movies);
        }





        // POST api/values
        [HttpPost("Rate")]
        public void Post([FromBody]RatingDTO rating)
        {
            _movieService.RateMovie(rating.movieId, rating.userId, rating.rate);
        }

        // PUT api/values/5
        //[HttpPut("Rate")]
        //public void Put(int Id, [FromBody] int value)
        //{
        //}

        // DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}


        private MovieViewModel viewModel(Movie movie)
        {
            return new MovieViewModel(
                id: movie.MovieId,
                title: movie.Title,
                genre: movie.Genre,
                yearOfRelease: movie.YearOfRelease,
                runningTimeInMinutes: movie.RunningTimeInMinutes,
                averageRating: _roundHelper.Round(movie.AverageRating),
                ratingCount: movie.RatingCount
            );
        }

        private List<MovieViewModel> viewModel(IEnumerable<Movie> movieList)
        {
            return movieList.Select(movie => viewModel(movie)).ToList();
        }

    }
}
