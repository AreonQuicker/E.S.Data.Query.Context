using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using E.S.Data.Query.Context.Attributes;
using E.S.Data.Query.Context.Enums;
using E.S.Data.Query.Context.Interfaces;
using E.S.Data.Query.DataAccess.Interfaces;

namespace E.S.Data.Query.Context.Queries
{
    public class DataCreateQuery : IDataCreateQuery, IDataCreateQueryInner
    {
        #region Fields

        private readonly IDataAccessQuery _dataAccessQuery;
        private readonly DynamicParameters _dynamicParameters;
        private readonly StringBuilder _sql;
        private bool _ignoreNullValues;
        private object _model;
        private string _schemaName;
        private string _tableName;
        private Type _type;

        #endregion

        #region Constructors

        public DataCreateQuery(IDataAccessQuery dataAccessQuery)
        {
            _dataAccessQuery = dataAccessQuery;
            _sql = new StringBuilder();
            _dynamicParameters = new DynamicParameters();
        }

        #endregion

        public string Sql => _sql.ToString();

        #region Private Methods

        private void BuildQuery()
        {
            _sql.Clear();

            var tableName = _tableName ?? _type.Name;
            var schema = _schemaName;

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

            _sql.Append("INSERT INTO");

            if (schema != null)
                _sql.Append($" {schema}.{tableName}");
            else
                _sql.Append($" {tableName}");

            var properties = _type.GetProperties()
                .ToList();

            _sql.Append(" (");

            var firstLoop = true;
            foreach (var propertyInfo in properties)
            {
                var pAttribute = (DataQueryContextPropertyAttribute) propertyInfo
                    .GetCustomAttributes(typeof(DataQueryContextPropertyAttribute), false)
                    .FirstOrDefault();

                var name = propertyInfo.Name;
                var value = propertyInfo.GetValue(_model);

                if (_ignoreNullValues && value == null)
                    continue;

                if (pAttribute != null)
                {
                    if (!pAttribute.Include)
                        continue;

                    if (!pAttribute.Flags.HasFlag(DataQueryContextPropertyFlags.Create))
                        continue;

                    name = pAttribute?.Name ?? name;
                }

                if (!firstLoop)
                    _sql.Append(",");

                _sql.Append($"[{name}]");

                firstLoop = false;
            }

            _sql.Append(")");

            _sql.Append(" VALUES(");

            firstLoop = true;
            foreach (var propertyInfo in properties)
            {
                var pAttribute = (DataQueryContextPropertyAttribute) propertyInfo
                    .GetCustomAttributes(typeof(DataQueryContextPropertyAttribute), false)
                    .FirstOrDefault();

                var name = propertyInfo.Name;
                var value = propertyInfo.GetValue(_model);

                if (_ignoreNullValues && value == null)
                    continue;


                if (pAttribute != null)
                {
                    if (!pAttribute.Include)
                        continue;

                    if (!pAttribute.Flags.HasFlag(DataQueryContextPropertyFlags.Create))
                        continue;

                    name = pAttribute?.Name ?? name;
                }

                if (!firstLoop)
                    _sql.Append(",");

                _sql.Append($"@{name}");
                _dynamicParameters.Add($"@{name}", value);

                firstLoop = false;
            }

            _sql.Append(")");
        }

        #endregion

        #region IDataCreateQuery

        public IDataCreateQueryInner Create(Type type, object model)
        {
            return new DataCreateQuery(_dataAccessQuery)
                .WithModel(type, model);
        }

        public IDataCreateQueryInner Create<T>(T model) where T : class, new()
        {
            return new DataCreateQuery(_dataAccessQuery)
                .WithModel(model);
        }

        public IDataCreateQueryInner WithModel(Type type, object model)
        {
            _type = type;
            _model = model;

            return this;
        }

        public IDataCreateQueryInner WithModel<T>(T model) where T : class, new()
        {
            _type = model.GetType();
            _model = model;

            return this;
        }

        public IDataCreateQueryInner WithTableName(string tableName)
        {
            _tableName = tableName;

            return this;
        }


        public IDataCreateQueryInner WithSchemaName(string schemaName)
        {
            _schemaName = schemaName;

            return this;
        }

        public IDataCreateQueryInner IgnoreNullValues(bool ignoreNullValues)
        {
            _ignoreNullValues = ignoreNullValues;

            return this;
        }

        public async Task<int> CreateAsync()
        {
            BuildQuery();
            _sql.Append(" SELECT CAST(SCOPE_IDENTITY() AS INT)");
            var data = await _dataAccessQuery.FirstOrDefaultQueryAsync<int>(_sql.ToString(), _dynamicParameters);

            return data;
        }

        #endregion
    }
}