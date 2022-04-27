using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E.S.Data.Query.Context.Interfaces
{
    public interface IDataSelectQueryInner
    {
        string Sql { get; }
        IDataSelectQueryInner Where(string key, object value);
        IDataSelectQueryInner WhereId(object value);
        
        IDataSelectQueryInner WithType(Type type);
        
        IDataSelectQueryInner WithType<T>() where T : class, new();
        IDataSelectQueryInner WithTableName(string tableName);

        IDataSelectQueryInner WithSchemaName(string schemaName);

        Task<IEnumerable<T>> ListAsync<T>();
        Task<T> FirstOrDefaultAsync<T>();
    }
}