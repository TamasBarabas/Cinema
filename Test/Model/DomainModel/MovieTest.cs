using Model.DomainModel;
using NUnit.Framework;
using System;

namespace Test.Model.DomainModel
{
    class DomainModel
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_ShouldHaveNoRating()
        {
            User user = new User("Tamas");
            Movie movie = new Movie("Title", GenreEnum.Adventure, 2000, 200);

            Assert.Zero(movie.Ratings.Count, "Ratings.Count don't match");
            Assert.Zero(movie.RatingCount, "RatingCount don't match");
            Assert.Zero(movie.AverageRating, "AverageRating don't match");
            Assert.Null(movie.RatingBy(user.UserId), "RatingBy is not null");

        }

        [Test]
        public void Test_FirstRateingShouldBeRetrieved()
        {
            User user = new User("Tamas");
            Movie movie = new Movie("Title", GenreEnum.Adventure, 2000, 200);
            movie.Rate(user, 5);
            Assert.AreEqual(1, movie.Ratings.Count, "Ratings.Count don't match");
            Assert.AreEqual(1, movie.RatingCount, "RatingCount don't match");
            Assert.AreEqual(5, movie.AverageRating, "AverageRating don't match");
            Assert.AreEqual(5, movie.RatingBy(user.UserId), "RatingBy don't match");
        }

        [Test]
        public void Test_ReratingShouldBeRetrieved()
        {
            User user = new User("Tamas");
            Movie movie = new Movie("Title", GenreEnum.Adventure, 2000, 200);

            //First rating
            movie.Rate(user, 5);
            Assert.AreEqual(1, movie.Ratings.Count, "Ratings.Count don't match before rerate");
            Assert.AreEqual(1, movie.RatingCount, "RatingCount don't match before rerate");
            Assert.AreEqual(5, movie.AverageRating, "AverageRating don't match before rerate");
            Assert.AreEqual(5, movie.RatingBy(user.UserId), "RatingBy don't match before rerate");

            //Second rating
            movie.Rate(user, 4);
            Assert.AreEqual(1, movie.Ratings.Count, "Ratings.Count don't match after rerate");
            Assert.AreEqual(1, movie.RatingCount, "RatingCount don't match after rerate");
            Assert.AreEqual(4, movie.AverageRating, "AverageRating don't match after rerate");
            Assert.AreEqual(4, movie.RatingBy(user.UserId), "RatingBy don't match after rerate");

        }

        [Test]
        public void Test_RatingByOtherPersonShouldWorkCorrectly()
        {
            User user1 = new User(1, "Tamas");
            User user2 = new User(2, "Edit");
            Movie movie = new Movie("Title", GenreEnum.Adventure, 2000, 200);

            //First rating
            movie.Rate(user1, 5);
            movie.Rate(user2, 4);

            Assert.AreEqual(2, movie.Ratings.Count, "Ratings.Count don't match");
            Assert.AreEqual(2, movie.RatingCount, "RatingCount don't match");
            Assert.AreEqual(4.5, movie.AverageRating, "AverageRating don't match");
            Assert.AreEqual(5, movie.RatingBy(user1.UserId), "RatingBy(user1) don't match");
            Assert.AreEqual(4, movie.RatingBy(user2.UserId), "RatingBy(user2) don't match");

        }

        [Test]
        public void Test_ShouldThrowExceptionForTooLargeRating()
        {
            User user = new User("Tamas");
            Movie movie = new Movie("Title", GenreEnum.Adventure, 2000, 200);
            Assert.Throws<ArgumentOutOfRangeException>(() => movie.Rate(user, 6));
        }

        [Test]
        public void Test_ShouldThrowExceptionForTooSmallRating()
        {
            User user = new User("Tamas");
            Movie movie = new Movie("Title", GenreEnum.Adventure, 2000, 200);
            Assert.Throws<ArgumentOutOfRangeException>(() => movie.Rate(user, 0));
        }

        
    }
}
