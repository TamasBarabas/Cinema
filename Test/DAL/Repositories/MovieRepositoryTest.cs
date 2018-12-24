using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;
using DAL;
using Model.Data;
using Model.DomainModel;
using DAL.Repositories;

namespace Test.DAL.Repositories
{
    public class MovieRepositoryTest
    {
        CinemaContext context;
        IMovieRepository repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<CinemaContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            context = new CinemaContext(options);
            context.Database.EnsureCreated();
            repository = new MovieRepository(context);
        }

        [Test]
        public void Test_GetOneOrDefaultShouldReturnTheCorrectMovieWithrating()
        {
            Movie movie = repository.GetOneOrDefault(2);

            Assert.AreEqual("The Godfather", movie.Title, "Titles doesn't match");
            Assert.NotZero(movie.RatingCount, "Rating counts doesn't match");
        }

        [Test]
        public void Test_GetOneOrDefaultShouldReturnNoMovie()
        {
            Movie movie = repository.GetOneOrDefault(100);

            Assert.IsNull(movie);
        }

        [Test]
        public void Test_GetOneShouldReturnTheCorrectMovieWithrating()
        {
            Movie movie = repository.GetOne(2);

            Assert.AreEqual("The Godfather", movie.Title, "Titles doesn't match");
            Assert.NotZero(movie.RatingCount, "Rating counts doesn't match");
        }

        [Test]
        public void Test_GetOneShouldThrowException()
        {
            Assert.Throws<KeyNotFoundException>(() => repository.GetOne(100));
        }



        [Test]
        public void Test_ShouldReturnAllMoviesWithRatings()
        {
            IEnumerable<Movie> movies = repository.GetAll();

            Assert.AreEqual(10, movies.Count(), "Movie counters does't match");
            Assert.NotZero(movies.ToList()[0].RatingCount, "Movie's rating is zero");
        }

        [Test]
        public void Test_ShouldReturnTheTop5RatedMovies()
        {
            IEnumerable<Movie> movies = repository.GetTopMoviesByAverageRating();

            Assert.AreEqual(5, movies.Count());
        }

        [Test]
        public void Test_ShouldReturnTheTop5RatedMoviesByCount()
        {
            IEnumerable<Movie> movies = repository.GetTopMoviesByRateCount();

            Assert.AreEqual(5, movies.Count());
        }


        [Test]
        public void Test_ShouldReturnTheTop2RatedMovies()
        {
            List<Movie> movies = repository.GetTopMoviesByAverageRating(2).ToList();

            Assert.AreEqual(2, movies.Count());

            Assert.AreEqual("Schindler's List", movies[0].Title, "First movie's title doesn't match ");
            Assert.AreEqual("The Godfather: Part II", movies[1].Title, "Second movie's title doesn't match ");

        }

        [Test]
        public void Test_ShouldReturnTheTop2RatedMoviesByCount()
        {
            List<Movie> movies = repository.GetTopMoviesByRateCount(2).ToList();

            Assert.AreEqual(2, movies.Count());

            Assert.AreEqual("Schindler's List", movies[0].Title, "First movie's title doesn't match ");
            Assert.AreEqual("The Dark Knight", movies[1].Title, "Second movie's title doesn't match ");
        }

        [Test]
        public void Test_NeverRadingUserShouldReturnEmptyList()
        {
            User user = new User(0, "TestUser");
            Assert.AreEqual(0, repository.GetOne(10).Ratings.Count());

        }

        [Test]
        public void Test_NewRateShouldBeSavedAndCanBeRetrieved()
        {
            User user = new User(15, "TestUser");

            repository.GetOne(10).Rate(user, 4);
            repository.SaveChanges();

            Assert.AreEqual(1, repository.GetOne(10).Ratings.Count(), "Rating counter doesn't match ");
            Assert.AreEqual(15, repository.GetOne(10).Ratings.First().UserId, "Usre ID doesn't match ");
            Assert.AreEqual(4, repository.GetOne(10).Ratings.First().Value, "Rating value doesn't match ");
        }

        [Test]
        public void Test_RateShouldBeUpdatedAndCanBeRetrieved()
        {
            User user = new User(15, "TestUser");

            //First rate
            repository.GetOne(10).Rate(user, 4);
            repository.SaveChanges();

            //Update
            repository.GetOne(10).Rate(user, 3);
            repository.SaveChanges();

            Assert.AreEqual(3, repository.GetOne(10).Ratings.First().Value);
        }

        [Test]
        public void Test_InvalidRateShouldThrowException()
        {
            User user = new User(15, "TestUser");

            Assert.Throws<ArgumentOutOfRangeException>(() => repository.GetOne(10).Rate(user, 6));
        }

    }
}