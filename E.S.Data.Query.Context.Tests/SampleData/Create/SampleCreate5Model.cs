using E.S.Data.Query.Context.Attributes;
using E.S.Data.Query.Context.Enums;

namespace E.S.Data.Query.Context.Tests.SampleData.Create
{
    [DataQueryContext("Sample5")]
    public class SampleCreate5Model
    {
        [DataQueryContextProperty(DataQueryContextPropertyFlags.Create)]
        public string Name { get; set; }

        [DataQueryContextProperty(DataQueryContextPropertyFlags.Create)]
        public string Surname { get; set; }

        [DataQueryContextProperty(DataQueryContextPropertyFlags.Create)]
        public string NickName { get; set; }
    }
}