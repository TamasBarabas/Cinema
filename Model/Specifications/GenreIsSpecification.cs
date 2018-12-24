using System;
using System.Linq.Expressions;
using Model.DomainModel;

namespace Model.Specifications
{
    public class GenreIsSpecification : Specification<Movie>
    {
        private readonly GenreEnum _genre;

        public GenreIsSpecification(GenreEnum? genre)
        {
            _genre = genre.Value;
        }

        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => (movie.Genre == _genre);
        }
    }
}
