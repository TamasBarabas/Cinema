using System;
using System.Linq;
using System.Linq.Expressions;
using Model.DomainModel;

namespace Model.Specifications
{
    public class RatedByUserSpecification : Specification<Movie>
    {
        private readonly int _userId;

        public RatedByUserSpecification(int userId)
        {
            _userId = userId;
            AddInclude(movie => movie.Ratings);

        }

        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => (movie.RatingBy(_userId).HasValue);
        }
    }
}
