using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using E.S.Data.Query.Context.Attributes;
using E.S.Data.Query.Context.Interfaces;
using E.S.Data.Query.Context.Models;
using E.S.Data.Query.DataAccess.Interfaces;

namespace E.S.Data.Query.Context.Queries
{
    public class DataSelectQuery : IDataSelectQuery, IDataSelectQueryInner
    {
        #region Fields

        private readonly IDataAccessQuery _dataAccessQuery;
        private readonly DynamicParameters _dynamicParameters;
        private readonly StringBuilder _sql;
        private Type _type;
        private string _schemaName;
        private string _tableName;
        private bool _selectAllFields = true;

        private readonly HashSet<(string Key, string KeyValue, string Compare, object Value, bool CastToDate)>
            _whereValues;

        private readonly Dictionary<string, string>
            _orderValues;

        private readonly List<JoinModel> _joins;

        #endregion

        public string Sql => _sql.ToString();

        #region Private Methods

        private void StartBuildQuery()
        {
            _sql.Clear();

            var tableName = GetTableAndSchemaName(out var schema);

            if (_selectAllFields)
            {
                _sql.Append("SELECT * FROM");
            }
            else
            {
                _sql.Append("SELECT ");

                var properties = _type.GetProperties()
                    .ToList();

                var firstLoop = true;
                foreach (var propertyInfo in properties)
                {
                    var name = propertyInfo.Name;

                    if (firstLoop)
                        _sql.Append($"[{name}]");
                    else
                        _sql.Append($",[{name}]");

                    firstLoop = false;
                }

                _sql.Append(" FROM");
            }

            if (schema != null)
                _sql.Append($" {schema}.{tableName}");
            else
                _sql.Append($" {tableName}");

            if (_whereValues.Any())
            {
                foreach (var join in _joins)
                {
                    _sql.Append(
                        $" JOIN DefaultEvent.CostAllocation ON CostAllocation.CreatedByUser = Security.CreatedByUser");
                }
            }

            foreach (var keyValuePair in _whereValues)
            {
                var key = keyValuePair.CastToDate ? $"CAST({keyValuePair.Key} AS DATE)" : $"[{keyValuePair.Key}]";

                if (!_sql.ToString().Contains("WHERE", StringComparison.InvariantCultureIgnoreCase))
                    _sql.Append($" WHERE {key} {keyValuePair.Compare ?? "="} @{keyValuePair.KeyValue}");
                else
                    _sql.Append($" AND {key} {keyValuePair.Compare ?? "="} @{keyValuePair.KeyValue}");
            }

            if (_orderValues.Any())
            {
                foreach (var orderValue in _orderValues)
                {
                    if (!_sql.ToString().Contains("ORDER BY", StringComparison.InvariantCultureIgnoreCase))
                        _sql.Append($" ORDER BY [{orderValue.Key}] {orderValue.Value}");
                    else
                        _sql.Append($",[{orderValue.Key}] {orderValue.Value}");
                }
            }
        }

        private string GetTableAndSchemaName(out string schema)
        {
            var tableName = _type.Name;
            schema = _schemaName;

            if (_type.GetCustomAttributes(typeof(DataQueryContextAttribute), false).FirstOrDefault() is
                DataQueryContextAttribute dataQueryAttribute)
            {
                if (dataQueryAttribute.GetName() is { } attributeTable)
                    tableName = attributeTable;

                if (dataQueryAttribute.GetSchema() is { } attributeSchema)
                    schema = attributeSchema;
            }

            if (_tableName != null)
                tableName = _tableName;

            return tableName;
        }

        #endregion

        #region Constructors

        public DataSelectQuery(IDataAccessQuery dataAccessQuery)
        {
            _dataAccessQuery = dataAccessQuery;
            _sql = new StringBuilder();
            _dynamicParameters = new DynamicParameters();
            _whereValues = new HashSet<(string Key, string KeyValue, string Compare, object Value, bool CastToDate)>();
            _orderValues = new Dictionary<string, string>();
            _joins = new List<JoinModel>();
        }

        #endregion

        #region IDataSelectQuerya

        public IDataSelectQueryInner Select(Type type)
        {
            return new DataSelectQuery(_dataAccessQuery)
                .WithType(type);
        }

        public IDataSelectQueryInner Select<T>() where T : class, new()
        {
            return new DataSelectQuery(_dataAccessQuery)
                .WithType<T>();
        }

        public IDataSelectQueryInner Where(string key, object value)
        {
            return Where(key, null, value);
        }

        public IDataSelectQueryInner Where(string key, string compare, object value, bool castToDate = false)
        {
            var existingKeys = _whereValues.Count(w => w.Key == key);
            var keyValue = $"{key}{(existingKeys + 1).ToString()}";

            _whereValues.Add((key, keyValue, compare, value, castToDate));

            _dynamicParameters.Add($"@{keyValue}", value);

            return this;
        }

        public IDataSelectQueryInner WhereId(object value)
        {
            Where("Id", value);

            return this;
        }

        public IDataSelectQueryInner Join(string fromTable, string fromTableField, string toTable, string toTableField)
        {
            _joins.Add(new JoinModel(fromTable, fromTableField, toTable, toTableField));

            return this;
        }

        public IDataSelectQueryInner Join(string toTable, string toTableField)
        {
            var tableName = GetTableAndSchemaName(out _);
            _joins.Add(new JoinModel(tableName, "Id", toTable, toTableField));

            return this;
        }

        public IDataSelectQueryInner WithType(Type type)
        {
            _type = type;

            return this;
        }

        public IDataSelectQueryInner WithType<T>() where T : class, new()
        {
            _type = typeof(T);

            return this;
        }

        public IDataSelectQueryInner WithTableName(string tableName)
        {
            _tableName = tableName;

            return this;
        }

        public IDataSelectQueryInner WithSchemaName(string schemaName)
        {
            _schemaName = schemaName;

            return this;
        }

        public IDataSelectQueryInner WithSelectAllFields(bool selectAllFields)
        {
            _selectAllFields = selectAllFields;

            return this;
        }

        public IDataSelectQueryInner OrderAsc(string key)
        {
            if (_orderValues.ContainsKey(key))
                _orderValues.Remove(key);

            _orderValues.Add(key, "ASC");

            return this;
        }

        public IDataSelectQueryInner OrderDesc(string key)
        {
            if (_orderValues.ContainsKey(key))
                _orderValues.Remove(key);

            _orderValues.Add(key, "DESC");

            return this;
        }

        public async Task<IEnumerable<T>> ListAsync<T>()
        {
            StartBuildQuery();

            var data = await _dataAccessQuery.ListQueryAsync<T>(_sql.ToString(), _dynamicParameters);

            return data;
        }

        public async Task<T> FirstOrDefaultAsync<T>()
        {
            StartBuildQuery();

            var data = await _dataAccessQuery.FirstOrDefaultQueryAsync<T>(_sql.ToString(), _dynamicParameters);

            return data;
        }

        #endregion
    }
}