using Model.DomainModel;
using Model.Exceptions;
using WebApi.DTO;

namespace Model.Specifications.Factories
{
    public class MovieSpecificationFactory
    {
        public ISpecification<Movie> CreateSpecification(MovieSearchCriteria searchCriteria)
        {
            ISpecification<Movie> specification = new EmptySpecification<Movie>();

            if (!string.IsNullOrEmpty(searchCriteria.Title))
                specification = specification.And(new TitleIncludesSpecification(searchCriteria.Title));
            if (searchCriteria.Genre.HasValue)
                specification = specification.And(new GenreIsSpecification(searchCriteria.Genre));
            if (searchCriteria.MinYearOfRelease.HasValue || searchCriteria.MaxYearOfRelease.HasValue)
                specification = specification.And(new YearOfReleaseBetweenSpecification(searchCriteria.MinYearOfRelease, searchCriteria.MaxYearOfRelease));
            if (searchCriteria.MinRunningTimeInMinutes.HasValue || searchCriteria.MaxRunningTimeInMinutes.HasValue)
                specification = specification.And(new RunningTimeBetweenSpecification(searchCriteria.MinRunningTimeInMinutes, searchCriteria.MaxRunningTimeInMinutes));

            if(specification.GetType() == typeof(EmptySpecification<Movie>))
            {
                throw new EmptySpecificationException("No filter criteria procided");
            }

            return specification;
        }
    }
}
