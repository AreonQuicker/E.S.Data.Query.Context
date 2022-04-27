using E.S.Data.Query.Context.Attributes;
using E.S.Data.Query.Context.Enums;

namespace E.S.Data.Query.Context.Tests.SampleData.Update
{
    [DataQueryContext("Sample2")]
    public class SampleUpdate2Model
    {
        [DataQueryContextProperty(DataQueryContextPropertyFlags.Update)]
        public string Name { get; set; }

        [DataQueryContextProperty(DataQueryContextPropertyFlags.Update)]
        public string Surname { get; set; }
        
        [DataQueryContextProperty(DataQueryContextPropertyFlags.None)]
        public string Ignore { get; set; }
        
        [DataQueryContextProperty(DataQueryContextPropertyFlags.None)]
        [DataQueryContextIdProperty()]
        public string Key { get; set; }
    }
}