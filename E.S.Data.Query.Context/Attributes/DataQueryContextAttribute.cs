using System;

namespace E.S.Data.Query.Context.Attributes
{
    /// <summary>
    /// This attribute will be used on model classes for SQL queries to specify the table name and schema if required
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class DataQueryContextAttribute : Attribute
    {
        private readonly string _name;
        private readonly string _schema;

        public DataQueryContextAttribute(string name = null, string schema = null)
        {
            _name = name;
            _schema = schema;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetSchema()
        {
            return _schema;
        }
    }
}