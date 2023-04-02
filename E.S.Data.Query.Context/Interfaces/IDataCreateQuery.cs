using System;

namespace E.S.Data.Query.Context.Interfaces
{
    /// <summary>
    /// Main data create query interface
    /// This will be used to create an entry from a model class in SQL by generating a SQL string and executing it on SQL server
    /// Builder pattern will be used for building a SQL string which will be executed on SQL to create an entry in the DB 
    /// </summary>
    public interface IDataCreateQuery
    {
        /// <summary>
        /// The entry point of the builder pattern to set the model which will be used for generating a create SQL string
        /// </summary>
        /// <param name="model">Specify the model for generation the SQL string</param>
        /// <typeparam name="T">Generic type of the model</typeparam>
        /// <returns>Return the next builder interface for chaining methods</returns>
        IDataCreateQueryInner Create<T>(T model) where T : class, new();

        /// <summary>
        /// The entry point of the builder pattern to set the model which will be used for generating a create SQL string
        /// </summary>
        /// <param name="type">Specify the model type for generation the SQL string</param>
        /// <param name="model">Specify the model for generation the SQL string</param>
        /// <returns>Return the next builder interface for chaining methods</returns>
        IDataCreateQueryInner Create(Type type, object model);
    }
}