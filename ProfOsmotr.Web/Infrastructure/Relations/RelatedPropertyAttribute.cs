using System;

namespace ProfOsmotr.Web.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RelatedPropertyAttribute : Attribute
    {
        public Type RelatedProperty { get; private set; }

        public RelatedPropertyAttribute(Type relatedProperty)
        {
            RelatedProperty = relatedProperty;
        }
    }
}