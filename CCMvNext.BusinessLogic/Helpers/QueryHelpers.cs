using System;
using System.Linq;
using System.Linq.Expressions;

namespace CCMvNext.BusinessLogic.Helpers
{
    public static class QueryHelpers
    {
        /// <summary>
        /// Unfortunately, MongoDB does not support DateTime.Date parameter. Please, use this to add GroupBy(x => x.DateTime.Date) capability to any query.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="dateExpression">An expression to get DateTime property: <code>x => x.Timestamp.Date</code></param>
        /// <returns></returns>
        public static IQueryable<IGrouping<string, TSource>> GroupByDate<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, DateTime>> dateExpression)
        {
            var stringContant = typeof(string).GetMethod("Concat", new[] { typeof(string), typeof(string) });

            var yearString = Expression.Call(Expression.Property(dateExpression.Body, "Year"), "ToString", Type.EmptyTypes); // x.Date.Year.ToString()
            var monthString = Expression.Call(Expression.Property(dateExpression.Body, "Month"), "ToString", Type.EmptyTypes); // x.Date.Month.ToString()
            var dayString = Expression.Call(Expression.Property(dateExpression.Body, "Day"), "ToString", Type.EmptyTypes); // x.Date.Day.ToString()

            var expression = Expression.Add(yearString, Expression.Constant("-"), stringContant); // x.Date.Year.ToString() + "-"
            expression = Expression.Add(expression, monthString, stringContant); // x.Date.Year.ToString() + "-" + x.Date.Month.ToString()
            expression = Expression.Add(expression, Expression.Constant("-"), stringContant); // x.Date.Year.ToString() + "-" + x.Date.Month.ToString() + "-"
            expression = Expression.Add(expression, dayString, stringContant); // x.Date.Year.ToString() + "-" + x.Date.Month.ToString() + "-" + x.Date.Day.ToString()

            var lambda = Expression.Lambda<Func<TSource, string>>(expression, dateExpression.Parameters); // x => x.Date.Year.ToString() + "-" + x.Date.Month.ToString() + "-" + x.Date.Day.ToString()

            return source.GroupBy(lambda);
        }
    }
}
