using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using DAL;
using System.Collections.Generic;
using Model.DomainModel;
using System.Linq;
using System;
using Model.Data;
using DAL.Repositories;

namespace Test.DAL.Repositories
{
    public class RepositoryTest
    {
        CinemaContext context;
        IRepository<Movie> repository;

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
        public void Test_ShouldReturnCorrectType()
        {
            var result = context.Movies.ToList();

            Assert.IsAssignableFrom<List<Movie>>(result);
        }


        [Test]
        public void Test_ContextShouldReturnListOfMovies()
        {
            var result = context.Movies.ToList();

            Assert.AreEqual(10, result.Count());
        }

        [Test]
        public void Test_GetOneOrDefaultShouldReturnTheCorrectMovie()
        {
            Movie movie = repository.GetOneOrDefault(2);

            Assert.AreEqual("The Godfather", movie.Title);
        }

        [Test]
        public void Test_GetOneOrDefaultShouldReturnNoMovie()
        {
            Movie movie = repository.GetOneOrDefault(100);

            Assert.IsNull(movie);
        }

        [Test]
        public void Test_GetOneShouldReturnTheCorrectMovie()
        {
            Movie movie = repository.GetOne(2);

            Assert.AreEqual("The Godfather", movie.Title);
        }

        [Test]
        public void Test_GetOneShouldThrowException()
        {
            Assert.Throws<KeyNotFoundException>(() => repository.GetOne(100));
        }


        [Test]
        public void Test_ShouldReturnAllMovies()
        {
            IEnumerable<Movie> movies = repository.GetAll();

            Assert.AreEqual(10, movies.Count());
        }

        [Test]
        public void Test_ShouldAddMovie()
        {
            Assert.AreEqual(10, repository.GetAll().Count());

            repository.Add(new Movie(11, "TestMovie1", GenreEnum.Action, 10, 20));

            context.SaveChanges();

            Assert.AreEqual(11, repository.GetAll().Count(), "Amount of mivies doesn't match ");

            Assert.AreEqual("TestMovie1", repository.GetOneOrDefault(11).Title, "Title doesn't match ");
        }

        [Test]
        public void Test_ShouldAddMovies()
        {
            Assert.AreEqual(10, repository.GetAll().Count());

            repository.AddRange(new List<Movie>() {
                new Movie(11, "TestMovie1", GenreEnum.Action, 10, 20),
                new Movie(12, "TestMovie2", GenreEnum.Action, 10, 20)
            });

            context.SaveChanges();

            Assert.AreEqual(12, repository.GetAll().Count(), "Amount of movies doesn't match ");

            Assert.AreEqual("TestMovie1", repository.GetOneOrDefault(11).Title, "First movie's title doesn't match ");
            Assert.AreEqual("TestMovie2", repository.GetOneOrDefault(12).Title, "Second movie's title doesn't match ");
        }

        [Test]
        public void Test_ShouldRemoveMovie()
        {
            Assert.AreEqual(10, repository.GetAll().Count());
            Movie movieToRemove = repository.GetOneOrDefault(1);

            repository.Remove(movieToRemove);
            context.SaveChanges();

            Assert.AreEqual(9, repository.GetAll().Count());
        }


        [Test]
        public void Test_ShouldRemoveMovies()
        {
            Assert.AreEqual(10, repository.GetAll().Count());

            List<Movie> movies = new List<Movie> { repository.GetOneOrDefault(1), repository.GetOneOrDefault(2) };
            repository.RemoveRange(movies);

            context.SaveChanges();

            Assert.AreEqual(8, repository.GetAll().Count());
        }



    }
}