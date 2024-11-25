using System.Linq.Expressions;

namespace BRANEF.Shared.Extensions
{
    public static class LinqExtensions
    {
        public static bool In<T>(this T source, params T[] values) => values.Contains(source);        
    }
}
