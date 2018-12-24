using System;
using System.Linq.Expressions;
using Model.DomainModel;

namespace Model.Specifications
{
    public class TitleIncludesSpecification : Specification<Movie>
    {
        private readonly string _partialTitle;

        public TitleIncludesSpecification(string partialTitle)
        {
            _partialTitle = partialTitle;
        }

        public override Expression<Func<Movie, bool>> ToExpression()
        {
            return movie => movie.Title.ToLower().Contains(_partialTitle.ToLower());
        }
    }
}
