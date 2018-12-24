using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Model.Specifications
{
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T candidate);
        List<Expression<Func<T, object>>> Includes { get; }
        List<string> IncludeStrings { get; }
        Expression<Func<T, bool>> ToExpression();
        ISpecification<T> And(ISpecification<T> other);
    }

}
