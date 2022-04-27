using E.S.Data.Query.Context.Attributes;

namespace E.S.Data.Query.Context.Tests.SampleData.Create
{
    [DataQueryContext("Sample1")]
    public class SampleCreate1Model
    {
        public string Name { get; set; }

        public string Surname { get; set; }
    }
}