using E.S.Data.Query.Context.Interfaces;
using E.S.Data.Query.Context.Queries;
using E.S.Data.Query.DataAccess.Interfaces;

namespace E.S.Data.Query.Context.Extensions
{
    /// <summary>
    /// Data create SQL query extensions to extend IDataAccessQuery
    /// </summary>
    public static class DataCreateQueryExtensions
    {
        public static IDataCreateQuery CreateQuery(this IDataAccessQuery dataAccessQuery)
        {
            return new DataCreateQuery(dataAccessQuery);
        }
        
        public static IDataCreateQueryInner CreateQuery<T>(this IDataAccessQuery dataAccessQuery, T model) where T : class,new()
        {
            return new DataCreateQuery(dataAccessQuery)
                .Create<T>(model);
        }
    }
}