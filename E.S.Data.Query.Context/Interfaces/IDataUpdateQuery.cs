using System;

namespace E.S.Data.Query.Context.Interfaces
{
    /// <summary>
    /// Main data update query interface
    /// This will be used to update an entry from a model class in SQL by generating a SQL string and executing it on SQL server
    /// Builder pattern will be used for building a SQL string which will be executed on SQL to update an entry in the DB 
    /// </summary>
    public interface IDataUpdateQuery
    {
        /// <summary>
        /// The entry point of the builder pattern to set the model which will be used for generating a update SQL string
        /// </summary>
        /// <param name="model">Specify the model for generation the SQL string</param>
        /// <typeparam name="T">Generic type of the model</typeparam>
        /// <returns>Return the next builder interface for chaining methods</returns>
        IDataUpdateQueryInner Update<T>(T model) where T : class, new();

        /// <summary>
        /// The entry point of the builder pattern to set the model which will be used for generating a update SQL string
        /// </summary>
        /// <param name="model">Specify the model for generation the SQL string</param>
        /// <typeparam name="T">Generic type of the model</typeparam>
        /// <returns>Return the next builder interface for chaining methods</returns>
        IDataUpdateQueryInner Update(Type type, object model);
    }
}