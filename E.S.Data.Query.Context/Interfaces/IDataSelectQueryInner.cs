using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E.S.Data.Query.Context.Interfaces
{
    public interface IDataSelectQueryInner
    {
        string Sql { get; }
        IDataSelectQueryInner Where(string key, object value);
        IDataSelectQueryInner Where(string key, string compare, object value, bool castToDate = false);
        
        IDataSelectQueryInner WhereId(object value);
        
        IDataSelectQueryInner WithType(Type type);
        
        IDataSelectQueryInner WithType<T>() where T : class, new();
        
        IDataSelectQueryInner OrderAsc(string key);
        IDataSelectQueryInner OrderDesc(string key);
        IDataSelectQueryInner WithTableName(string tableName);

        IDataSelectQueryInner WithSchemaName(string schemaName);
        IDataSelectQueryInner WithSelectAllFields(bool selectAllFields);

        Task<IEnumerable<T>> ListAsync<T>();
        Task<T> FirstOrDefaultAsync<T>();
   
    }
}