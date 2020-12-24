using System;
using System.Linq.Expressions;

namespace ProfOsmotr.Web.Infrastructure
{
    public class PropertyRelation<TOwner, TProperty>
    {
        public Expression<Func<TOwner, TProperty>> Expression { get; }

        public PropertyRelation(Expression<Func<TOwner, TProperty>> expression)
        {
            Expression = expression;
        }
    }
}