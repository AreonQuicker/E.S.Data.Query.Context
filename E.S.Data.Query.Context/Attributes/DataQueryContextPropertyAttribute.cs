using System;
using E.S.Data.Query.Context.Enums;

namespace E.S.Data.Query.Context.Attributes
{
    /// <summary>
    /// This attribute will be used on properties on model classes to specify if the property needs to be included in SQL queries and the name if required
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DataQueryContextPropertyAttribute : Attribute
    {
        public DataQueryContextPropertyAttribute(DataQueryContextPropertyFlags flags, string name = null,
            bool include = true)
        {
            Include = include;
            Name = name;
            Flags = flags;
        }

        public string Name { get; }

        public bool Include { get; }

        public DataQueryContextPropertyFlags Flags { get; }
    }
}