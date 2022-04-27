using E.S.Data.Query.Context.Attributes;
using E.S.Data.Query.Context.Enums;

namespace E.S.Data.Query.Context.Tests.SampleData.Create
{
    [DataQueryContext("Sample2")]
    public class SampleCreate2Model
    {
        [DataQueryContextProperty(DataQueryContextPropertyFlags.Create)]
        public string Name { get; set; }

        [DataQueryContextProperty(DataQueryContextPropertyFlags.Update)]
        public string Surname { get; set; }
    }
}