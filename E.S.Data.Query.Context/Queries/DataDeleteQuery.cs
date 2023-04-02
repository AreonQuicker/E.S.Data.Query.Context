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
    public class DataDeleteQuery : IDataDeleteQuery, IDataDeleteQueryInner
    {
        #region Fields

        private readonly IDataAccessQuery _dataAccessQuery;
        private readonly DynamicParameters _dynamicParameters;
        private readonly StringBuilder _sql;
        private object _model;
        private string _schemaName;
        private string _tableName;
        private Type _type;
        private object _idValue;
        private string _idkey;

        #endregion

        #region Constructors

        public DataDeleteQuery(IDataAccessQuery dataAccessQuery)
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

            _sql.Append("DELETE FROM");

            if (schema != null)
                _sql.Append($" {schema}.{tableName}");
            else
                _sql.Append($" {tableName}");

            var properties = _type.GetProperties()
                .ToList();

            var idKey = "Id";
            object idValue = null;

            foreach (var propertyInfo in properties)
            {
                var idPAttribute = (DataQueryContextIdPropertyAttribute) propertyInfo
                    .GetCustomAttributes(typeof(DataQueryContextIdPropertyAttribute), false)
                    .FirstOrDefault();

                var name = propertyInfo.Name;
                var value = propertyInfo.GetValue(_model);

                if (idPAttribute != null)
                {
                    idKey = idPAttribute.IdKey ?? name;
                    idValue = value;
                }
            }

            if (_idkey != null)
                idKey = _idkey;

            if (_idValue != null)
                idValue = _idValue;
            
            _sql.Append($" WHERE {idKey} = @{idKey}");
            _dynamicParameters.Add($"@{idKey}", idValue);
        }

        #endregion

        #region IDataUpdateQuery

        public IDataDeleteQueryInner Delete(Type type, object model)
        {
            return new DataDeleteQuery(_dataAccessQuery)
                .WithModel(type, model);
        }

        public IDataDeleteQueryInner Delete<T>(T model) where T : class, new()
        {
            return new DataDeleteQuery(_dataAccessQuery)
                .WithModel(model);
        }
        public IDataDeleteQueryInner WithModel(Type type, object model)
        {
            _type = type;
            _model = model;

            return this;
        }
        public IDataDeleteQueryInner WithModel<T>(T model) where T : class, new()
        {
            _type = model.GetType();
            _model = model;

            return this;
        }

        public IDataDeleteQueryInner WithTableName(string tableName)
        {
            _tableName = tableName;

            return this;
        }

        public IDataDeleteQueryInner WithSchemaName(string schemaName)
        {
            _schemaName = schemaName;

            return this;
        }

        public IDataDeleteQueryInner WithIdValue(object idValue)
        {
            _idValue = idValue;

            return this;
        }

        public IDataDeleteQueryInner WithIdKey(string idKey)
        {
            _idkey = idKey;

            return this;
        }
       
        public async Task DeleteAsync()
        {
            //TODO: Complete the id field
            BuildQuery();
            _sql.Append(" SELECT 1");
            var id = await _dataAccessQuery.FirstOrDefaultQueryAsync<int>(_sql.ToString(), _dynamicParameters);
        }

        #endregion
    }
}