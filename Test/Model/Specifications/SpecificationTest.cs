using NUnit.Framework;
using Model.DomainModel;
using Model.Specifications;
using DAL;

namespace Test.Model.Specification
{

    public class SpecificationTest
    {

        static object[] specifications =
        {
            new object[] { new YearOfReleaseBetweenSpecification(1994, 1994) },
            new object[] { new RunningTimeBetweenSpecification(140, 145) },
            new object[] { new GenreIsSpecification(GenreEnum.Drama) },
            new object[] { new RatedByUserSpecification(DbSeed.Users[0].UserId) },
            new object[] { new AverageRateBetweenSpecification(3, 5) },
            new object[] { new TitleIncludesSpecification("Shawshank") }
        };

        ISpecification<Movie> complexSpecification =
            new TitleIncludesSpecification("The Shawshank Redemption")
            .And(new GenreIsSpecification(GenreEnum.Drama))
            .And(new YearOfReleaseBetweenSpecification(1994, 1994))
            .And(new RunningTimeBetweenSpecification(140, 145))
            .And(new RatedByUserSpecification(DbSeed.Users[0].UserId))
            .And(new AverageRateBetweenSpecification(3, 5));


        [SetUp]
        public void Setup()
        {

        }

        [Test, TestCaseSource(nameof(specifications))]
        public void Test_MovieShouldSatisfySimpleSpecification(Specification<Movie> specification)
        {
            Movie testMovie = new Movie(1, "The Shawshank Redemption", GenreEnum.Drama, 1994, 142);
            testMovie.Rate(DbSeed.Users[0], 4);

            Assert.IsTrue(specification.IsSatisfiedBy(testMovie));
        }

        [Test, TestCaseSource(nameof(specifications))]
        public void Test_MovieShouldNotSatisfySimpleSpecification(Specification<Movie> specification)
        {
            Movie testMovie = new Movie(2, "The Godfather", GenreEnum.Crime, 1972, 175);

            Assert.IsFalse(specification.IsSatisfiedBy(testMovie));
        }

        [Test]
        public void Test_MovieShouldSatisfyComplexSpecification()
        {
            Movie testMovie = new Movie(1, "The Shawshank Redemption", GenreEnum.Drama, 1994, 142);
            testMovie.Rate(DbSeed.Users[0], 4);

            Assert.IsTrue(complexSpecification.IsSatisfiedBy(testMovie));
        }

        [Test]
        public void Test_MovieShouldNotSatisfyComplexSpecification()
        {
            Movie testMovie = new Movie(2, "The Godfather", GenreEnum.Crime, 1972, 175);

            Assert.IsFalse(complexSpecification.IsSatisfiedBy(testMovie));
        }


        [TestCase(0, 2018)]
        [TestCase(1000, 3000)]
        public void Test_AllMoviesShouldSatisfyYearOfReleaseBetweenSpecification(int min, int max)
        {
            ISpecification<Movie> specification = new YearOfReleaseBetweenSpecification(min, max);
            foreach (Movie movie in DbSeed.Movies)
            {
                Assert.IsTrue(specification.IsSatisfiedBy(movie));
            }
        }

        [TestCase(2018, 0)]
        [TestCase(3000, 1000)]
        public void Test_NoMovieShouldSatisfyYearOfReleaseBetweenSpecification(int min, int max)
        {
            ISpecification<Movie> specification = new YearOfReleaseBetweenSpecification(min, max);
            foreach (Movie movie in DbSeed.Movies)
            {
                Assert.IsFalse(specification.IsSatisfiedBy(movie));
            }

        }

    }
}