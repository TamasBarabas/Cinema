using System;
using System.Linq.Expressions;
using Model.DomainModel;

namespace Model.Specifications
{
    public class YearOfReleaseBetweenSpecification : AbstractRangeSpecification<Movie>
    {

        public YearOfReleaseBetweenSpecification(int? minimum, int? maximum)
            : base(minimum, maximum)
        {}

        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => (movie.YearOfRelease >= minimum && movie.YearOfRelease <= maximum);
        }
    }
}
