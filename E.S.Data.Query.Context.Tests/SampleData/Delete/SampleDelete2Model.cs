using E.S.Data.Query.Context.Attributes;

namespace E.S.Data.Query.Context.Tests.SampleData.Delete
{
    [DataQueryContext("Sample2")]
    public class SampleDelete2Model
    {
        public string Name { get; set; }
        
        [DataQueryContextIdProperty()]
        public string Key { get; set; }
    }
}