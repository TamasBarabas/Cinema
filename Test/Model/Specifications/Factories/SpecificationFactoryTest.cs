using NUnit.Framework;
using Model.DomainModel;
using Model.Specifications.Factories;
using WebApi.DTO;
using Model.Specifications;
using Model.Exceptions;

namespace Test.Model.Specification.Factories
{
    public class SpecificationFactoryTest
    {
        MovieSpecificationFactory factory = new MovieSpecificationFactory();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_ReturnEmptySpecificationForEmptySearchCriteria()
        {
            MovieSearchCriteria searchCriteria = new MovieSearchCriteria();

            Assert.Throws<EmptySpecificationException>(() => factory.CreateSpecification(searchCriteria));
        }

        [Test]
        public void Test_ReturnNonEmptySpecificationForNonEmptySearchCriteria()
        {
            MovieSearchCriteria searchCriteria = new MovieSearchCriteria()
            {
                Title = "godfather"
            };

            ISpecification<Movie> specification = factory.CreateSpecification(searchCriteria);

            Assert.IsNotAssignableFrom<EmptySpecification<Movie>>(specification);
        }

    }
}