using E.S.Data.Query.Context.Attributes;

namespace E.S.Data.Query.Context.Tests.SampleData.Update
{
    [DataQueryContext("Sample1")]
    public class SampleUpdate1Model
    {
        public string Name { get; set; }

        public string Surname { get; set; }
    }
}