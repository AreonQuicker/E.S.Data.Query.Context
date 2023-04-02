using System;
using System.Threading.Tasks;

namespace E.S.Data.Query.Context.Interfaces
{
    /// <summary>
    /// Inner data delete query interface
    /// This will be used to delete the entry from a model class in SQL by generating a SQL string and executing it on SQL server
    /// Builder pattern will be used for building a SQL string which will be executed on SQL to delete the entry in the DB 
    /// </summary>
    public interface IDataDeleteQueryInner
    {
        string Sql { get; }
        
        /// <summary>
        /// On the builder pattern to set the model which will be used for generating a delete SQL string
        /// </summary>
        /// <param name="type">Specify the model type for generation the SQL string</param>
        /// <param name="model">Specify the model for generation the SQL string</param>
        /// <returns>Return the current builder interface for chaining methods</returns>
        IDataDeleteQueryInner WithModel(Type type, object model);

        /// <summary>
        /// On the builder pattern to set the model type which will be used for generating a delete SQL string
        /// </summary>
        /// <param name="model">Specify the model for generation the SQL string</param>
        /// <typeparam name="T">Specify the generic model type for generation the SQL string</typeparam>
        /// <returns>Return the current builder interface for chaining methods</returns>
        IDataDeleteQueryInner WithModel<T>(T model) where T : class, new();

        /// <summary>
        /// On the builder pattern to set the table name which will be used for generating a delete SQL string
        /// </summary>
        /// <param name="tableName">Specify the table name for generation the SQL string</param>
        /// <returns>Return the current builder interface for chaining methods</returns>
        IDataDeleteQueryInner WithTableName(string tableName);

        /// <summary>
        /// On the builder pattern to set the schema name which will be used for generating a delete SQL string
        /// </summary>
        /// <param name="schemaName">Specify the table name for generation the SQL string</param>
        /// <returns>Return the current builder interface for chaining methods</returns>
        IDataDeleteQueryInner WithSchemaName(string schemaName);
        
        /// <summary>
        /// On the builder pattern to set the id value which will be used for generating a delete SQL string
        /// </summary>
        /// <param name="idValue">Specify the id value for generation the SQL string</param>
        /// <returns>Return the current builder interface for chaining methods</returns>
        IDataDeleteQueryInner WithIdValue(object idValue);
        
        /// <summary>
        /// On the builder pattern to set the id key which will be used for generating a delete SQL string
        /// </summary>
        /// <param name="idKey">Specify the id key for generation the SQL string</param>
        /// <returns>Return the current builder interface for chaining methods</returns>
        IDataDeleteQueryInner WithIdKey(string idKey);
        
        /// <summary>
        /// On the builder pattern, go and delete the entry in the db by using the generated SQL string
        /// </summary>
        /// <returns></returns>
        Task DeleteAsync();
    }
}