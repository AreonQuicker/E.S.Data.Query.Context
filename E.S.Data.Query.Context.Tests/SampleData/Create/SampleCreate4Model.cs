using E.S.Data.Query.Context.Attributes;
using E.S.Data.Query.Context.Enums;

namespace E.S.Data.Query.Context.Tests.SampleData.Create
{
    [DataQueryContext("Sample4")]
    public class SampleCreate4Model
    {
        [DataQueryContextProperty(DataQueryContextPropertyFlags.Create, "UName")]
        public string Name { get; set; }

        [DataQueryContextProperty(DataQueryContextPropertyFlags.Create, "USurname")]
        public string Surname { get; set; }
    }
}