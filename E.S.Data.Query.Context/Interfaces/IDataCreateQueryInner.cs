using System;
using System.Threading.Tasks;

namespace E.S.Data.Query.Context.Interfaces
{
    /// <summary>
    /// Inner data create query interface
    /// This will be used to create an entry from a model class in SQL by generating a SQL string and executing it on SQL server
    /// Builder pattern will be used for building a SQL string which will be executed on SQL to create an entry in the DB 
    /// </summary>
    public interface IDataCreateQueryInner
    {
        string Sql { get; }
        
        /// <summary>
        /// On the builder pattern to set the model which will be used for generating a create SQL string
        /// </summary>
        /// <param name="type">Specify the model type for generation the SQL string</param>
        /// <param name="model">Specify the model for generation the SQL string</param>
        /// <returns>Return the current builder interface for chaining methods</returns>
        IDataCreateQueryInner WithModel(Type type, object model);

        /// <summary>
        /// On the builder pattern to set the model type which will be used for generating a create SQL string
        /// </summary>
        /// <param name="model">Specify the model for generation the SQL string</param>
        /// <typeparam name="T">Specify the generic model type for generation the SQL string</typeparam>
        /// <returns>Return the current builder interface for chaining methods</returns>
        IDataCreateQueryInner WithModel<T>(T model) where T : class, new();

        /// <summary>
        /// On the builder pattern to set the table name which will be used for generating a create SQL string
        /// </summary>
        /// <param name="tableName">Specify the table name for generation the SQL string</param>
        /// <returns>Return the current builder interface for chaining methods</returns>
        IDataCreateQueryInner WithTableName(string tableName);

        /// <summary>
        /// On the builder pattern to set the schema name which will be used for generating a create SQL string
        /// </summary>
        /// <param name="schemaName">Specify the table name for generation the SQL string</param>
        /// <returns>Return the current builder interface for chaining methods</returns>
        IDataCreateQueryInner WithSchemaName(string schemaName);

        /// <summary>
        /// On the builder pattern to set if null values should be ignored for generating a create SQL string
        /// </summary>
        /// <param name="ignoreNullValues">Specify the ignore values field for generation the SQL string</param>
        /// <returns>Return the current builder interface for chaining methods</returns>
        IDataCreateQueryInner IgnoreNullValues(bool ignoreNullValues);
        
        /// <summary>
        /// On the builder pattern, go and create the entry in the db by using the generated SQL string
        /// </summary>
        /// <returns>Return the new created id of the entry</returns>
        Task<int> CreateAsync();
    }
}