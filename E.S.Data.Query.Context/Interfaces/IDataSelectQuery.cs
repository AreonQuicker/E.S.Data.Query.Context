using System;

namespace E.S.Data.Query.Context.Interfaces
{
    /// <summary>
    /// Main data select query interface
    /// This will be used to select data from SQL by generating a SQL string and executing it on SQL server
    /// Builder pattern will be used for building a SQL string which will be executed on SQL to select data from the DB 
    /// </summary>
    public interface IDataSelectQuery
    {
        /// <summary>
        /// The entry point of the builder pattern to set the model which will be used for generating a select SQL string
        /// </summary>
        /// <param name="model">Specify the model for generation the SQL string</param>
        /// <typeparam name="T">Generic type of the model</typeparam>
        /// <returns>Return the next builder interface for chaining methods</returns>
        IDataSelectQueryInner Select<T>() where T : class, new();

        /// <summary>
        /// The entry point of the builder pattern to set the model which will be used for generating a select SQL string
        /// </summary>
        /// <param name="type">Specify the model type for generation the SQL string</param>
        /// <param name="model">Specify the model for generation the SQL string</param>
        /// <returns>Return the next builder interface for chaining methods</returns>
        IDataSelectQueryInner Select(Type type);
    }
}