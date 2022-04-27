using System;

namespace E.S.Data.Query.Context.Enums
{
    /// <summary>
    /// This flag is used on the property attribute (DataQueryContextPropertyAttribute) to specify for which SQL query it will be used for
    /// </summary>
    [Flags]
    public enum DataQueryContextPropertyFlags
    {
        Create = 1,
        Update = 2,
        None = 0
    }
}