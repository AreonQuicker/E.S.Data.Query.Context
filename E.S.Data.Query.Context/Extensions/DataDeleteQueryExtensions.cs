using E.S.Data.Query.Context.Interfaces;
using E.S.Data.Query.Context.Queries;
using E.S.Data.Query.DataAccess.Interfaces;

namespace E.S.Data.Query.Context.Extensions
{
    /// <summary>
    /// Data delete SQL query extensions to extend IDataAccessQuery
    /// </summary>
    public static class DataDeleteQueryExtensions
    {
        public static IDataDeleteQuery DeleteQuery(this IDataAccessQuery dataAccessQuery)
        {
            return new DataDeleteQuery(dataAccessQuery);
        }
        
        public static IDataDeleteQueryInner DeleteQuery<T>(this IDataAccessQuery dataAccessQuery, T model) where T : class,new()
        {
            return new DataDeleteQuery(dataAccessQuery)
                .Delete<T>(model);
        }
    }
}