using System;
using System.Linq.Expressions;
using Model.DomainModel;

namespace Model.Specifications
{
    public class AverageRateBetweenSpecification : AbstractRangeSpecification<Movie>
    {
        public AverageRateBetweenSpecification(int? minimum, int? maximum)
            : base(minimum, maximum)
        {
            AddInclude(movie => movie.Ratings);
        }

        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => (movie.AverageRating >= minimum && movie.AverageRating <= maximum);
        }
    }
}
