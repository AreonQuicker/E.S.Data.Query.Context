namespace E.S.Data.Query.Context.Models
{
    public class JoinModel
    {
        public JoinModel(string fromTable, string fromTableField, string toTable, string toTableField, params string[] fieldsToInlcude)
        {
            FromTable = fromTable;
            ToTable = toTable;
            FromTableField = fromTableField;
            ToTableField = toTableField;
            FieldsToInlcude = fieldsToInlcude;
        }

        public string FromTable { get; set; }
        public string FromTableField { get; set; }
        public string ToTable { get; set; }
        public string ToTableField { get; set; }
        public string[] FieldsToInlcude { get; set; }
    }
}