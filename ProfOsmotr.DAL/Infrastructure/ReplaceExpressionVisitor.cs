﻿using System.Linq.Expressions;

namespace ProfOsmotr.DAL
{
    internal class ReplaceExpressionVisitor : ExpressionVisitor
    {
        private readonly Expression oldValue;
        private readonly Expression newValue;

        public ReplaceExpressionVisitor(Expression oldValue, Expression newValue)
        {
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        public override Expression Visit(Expression node)
        {
            if (node == oldValue)
                return newValue;
            return base.Visit(node);
        }
    }
}