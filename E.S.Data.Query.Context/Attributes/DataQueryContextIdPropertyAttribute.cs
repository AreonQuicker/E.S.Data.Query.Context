using System;

namespace E.S.Data.Query.Context.Attributes
{
    /// <summary>
    /// This attribute will be used for Update and Delete SQL queries to specify the key name on a property
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DataQueryContextIdPropertyAttribute : Attribute
    {
        public DataQueryContextIdPropertyAttribute(string idKey = null)
        {
            IdKey = idKey;
        }

        public string IdKey { get; }

    }
}