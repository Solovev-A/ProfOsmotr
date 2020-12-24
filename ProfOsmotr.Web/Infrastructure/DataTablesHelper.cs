using ProfOsmotr.Web.Models.DataTables;
using System;
using System.Linq;
using System.Reflection;

namespace ProfOsmotr.Web.Infrastructure
{
    internal static class DataTablesHelper
    {
        internal static DataTablesQuery GetQuery(Type viewModel, DataTablesParameters parameters)
        {
            if (!parameters.OrderingRequired)
                throw new NotSupportedException("Запросы без сортировки не поддерживаются");

            string search = null;
            if (parameters.SearchRequired)
            {
                search = parameters.Search.Value;
            }

            dynamic expression = null;
            DTOrder orderingParameter = parameters.Order.SingleOrDefault();
            if (orderingParameter == null)
                throw new NotSupportedException("Сортировка по нескольким параметрам не поддерживается");

            string orderingPropertyName = parameters.Columns[orderingParameter.Column].Name;
            try
            {
                expression = GetRelatedPropertyExpression(viewModel, orderingPropertyName);
            }
            catch
            {
                throw;
            }
            bool descending = orderingParameter.Dir == DTOrderDir.DESC;

            return new DataTablesQuery()
            {
                Start = parameters.Start,
                Length = parameters.Length,
                Search = search,
                OrderingSelector = expression,
                Descending = descending
            };
        }

        internal static dynamic GetRelatedPropertyExpression(Type viewModel, string viewModelPropertyName)
        {
            PropertyInfo property = GetProperty(viewModel, viewModelPropertyName);
            if (property == null)
                throw new InvalidOperationException("Свойство с таким именем не найдено");

            var relatedAttribute = (RelatedPropertyAttribute)property
                .GetCustomAttributes(typeof(RelatedPropertyAttribute), false)
                .SingleOrDefault();
            if (relatedAttribute == null)
                throw new InvalidOperationException("Атрибут установленного свойства отсутствует, либо задано более одного атрибута этого типа для данного свойства");

            Type relationObjectType = relatedAttribute.RelatedProperty;
            dynamic relationObject = Activator.CreateInstance(relationObjectType);
            return relationObject.Expression;
        }

        private static PropertyInfo GetProperty(Type owner, string name)
        {
            string[] props = name.Split('.');
            PropertyInfo result = null;
            foreach (var prop in props)
            {
                result = owner.GetProperty(prop);
                if (result == null)
                    return null;
                owner = result.PropertyType;
            }
            return result;
        }
    }
}