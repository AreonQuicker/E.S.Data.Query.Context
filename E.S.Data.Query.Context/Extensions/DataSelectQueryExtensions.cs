using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using E.S.Data.Query.Context.Interfaces;
using E.S.Data.Query.Context.Queries;
using E.S.Data.Query.DataAccess.Interfaces;
using Mapster;

namespace E.S.Data.Query.Context.Extensions
{
    /// <summary>
    /// Data select SQL query extensions to extend IDataAccessQuery and model classes
    /// </summary>
    public static class DataSelectQueryExtensions
    {
        public static IDataSelectQuery SelectQuery(this IDataAccessQuery dataAccessQuery)
        {
            return new DataSelectQuery(dataAccessQuery);
        }
        
        public static IDataSelectQueryInner SelectQuery<T>(this IDataAccessQuery dataAccessQuery) where T : class,new()
        {
            return new DataSelectQuery(dataAccessQuery)
                .Select<T>();
        }
    }
}