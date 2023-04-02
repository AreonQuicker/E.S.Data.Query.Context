using System;
using System.Threading.Tasks;

namespace E.S.Data.Query.Context.Interfaces
{
    /// <summary>
    /// Inner data update query interface
    /// This will be used to update the entry from a model class in SQL by generating a SQL string and executing it on SQL server
    /// Builder pattern will be used for building a SQL string which will be executed on SQL to update the entry in the DB 
    /// </summary>
    public interface IDataUpdateQueryInner
    {
        string Sql { get; }
        
        /// <summary>
        /// On the builder pattern to set the model which will be used for generating a update SQL string
        /// </summary>
        /// <param name="type">Specify the model type for generation the SQL string</param>
        /// <param name="model">Specify the model for generation the SQL string</param>
        /// <returns>Return the current builder interface for chaining methods</returns>
        IDataUpdateQueryInner WithModel(Type type, object model);

        /// <summary>
        /// On the builder pattern to set the model type which will be used for generating a update SQL string
        /// </summary>
        /// <param name="model">Specify the model for generation the SQL string</param>
        /// <typeparam name="T">Specify the generic model type for generation the SQL string</typeparam>
        /// <returns>Return the current builder interface for chaining methods</returns>
        IDataUpdateQueryInner WithModel<T>(T model) where T : class, new();

        /// <summary>
        /// On the builder pattern to set the table name which will be used for generating a update SQL string
        /// </summary>
        /// <param name="tableName">Specify the table name for generation the SQL string</param>
        /// <returns>Return the current builder interface for chaining methods</returns>
        IDataUpdateQueryInner WithTableName(string tableName);

        /// <summary>
        /// On the builder pattern to set the schema name which will be used for generating a update SQL string
        /// </summary>
        /// <param name="schemaName">Specify the table name for generation the SQL string</param>
        /// <returns>Return the current builder interface for chaining methods</returns>
        IDataUpdateQueryInner WithSchemaName(string schemaName);
        
        /// <summary>
        /// On the builder pattern to set the id value which will be used for generating a update SQL string
        /// </summary>
        /// <param name="idValue">Specify the id value for generation the SQL string</param>
        /// <returns>Return the current builder interface for chaining methods</returns>
        IDataUpdateQueryInner WithIdValue(object idValue);
        
        /// <summary>
        /// On the builder pattern to set the id key which will be used for generating a update SQL string
        /// </summary>
        /// <param name="idKey">Specify the id key for generation the SQL string</param>
        /// <returns>Return the current builder interface for chaining methods</returns>
        IDataUpdateQueryInner WithIdKey(string idKey);

        /// <summary>
        /// On the builder pattern to set if the values needs to a upsert query which will be used for generating a update SQL string
        /// </summary>
        /// <param name="upsert">Specify the upsert bool for generation the SQL string</param>
        /// <returns>Return the current builder interface for chaining methods</returns>
        IDataUpdateQueryInner WithUpsert(bool upsert);

        /// <summary>
        /// On the builder pattern to set if null values should be ignored for generating a update SQL string
        /// </summary>
        /// <param name="ignoreNullValues">Specify the ignore values field for generation the SQL string</param>
        /// <returns>Return the current builder interface for chaining methods</returns>
        IDataUpdateQueryInner IgnoreNullValues(bool ignoreNullValues);
        
        /// <summary>
        /// On the builder pattern, go and update the entry in the db by using the generated SQL string
        /// </summary>
        /// <returns></returns>
        Task UpdateAsync();
    }
}