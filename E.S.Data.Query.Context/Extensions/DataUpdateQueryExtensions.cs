using E.S.Data.Query.Context.Interfaces;
using E.S.Data.Query.Context.Queries;
using E.S.Data.Query.DataAccess.Interfaces;

namespace E.S.Data.Query.Context.Extensions
{
    /// <summary>
    /// Data update SQL query extensions to extend IDataAccessQuery
    /// </summary>
    public static class DataUpdateQueryExtensions
    {
        public static IDataUpdateQuery UpdateQuery(this IDataAccessQuery dataAccessQuery)
        {
            return new DataUpdateQuery(dataAccessQuery);
        }

        public static IDataUpdateQueryInner UpdateQuery<T>(this IDataAccessQuery dataAccessQuery, T model)
            where T : class, new()
        {
            return new DataUpdateQuery(dataAccessQuery)
                .Update<T>(model);
        }
    }
}