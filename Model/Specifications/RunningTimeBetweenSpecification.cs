using System;
using System.Linq.Expressions;
using Model.DomainModel;

namespace Model.Specifications
{
    public class RunningTimeBetweenSpecification : AbstractRangeSpecification<Movie>
    {

        public RunningTimeBetweenSpecification(int? minimum, int? maximum)
            :base(minimum, maximum)
        {
        }

        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => (movie.RunningTimeInMinutes >= minimum && movie.RunningTimeInMinutes <= maximum);
        }

    }
}
