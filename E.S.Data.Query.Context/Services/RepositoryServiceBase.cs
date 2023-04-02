using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using E.S.Data.Query.Context.Extensions;
using E.S.Data.Query.Context.Interfaces;
using E.S.Data.Query.DataAccess.Interfaces;

namespace E.S.Data.Query.Context.Services
{
    public abstract class RepositoryServiceBase<T> : IRepositoryService<T> where T : class, new()
    {
        private readonly IDataAccessQuery _dataAccessQuery;

        protected RepositoryServiceBase(IDataAccessQuery dataAccessQuery)
        {
            _dataAccessQuery = dataAccessQuery;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var result = await
                _dataAccessQuery
                    .SelectQuery()
                    .Select<T>()
                    .WhereId(id)
                    .FirstOrDefaultAsync<T>();

            return result;
        }

        public virtual async Task<T> GetByCodeAsync(string code)
        {
            var result = await
                _dataAccessQuery
                    .SelectQuery()
                    .Select<T>()
                    .Where("Code", code)
                    .FirstOrDefaultAsync<T>();

            return result;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var result = await
                _dataAccessQuery
                    .SelectQuery()
                    .Select<T>()
                    .ListAsync<T>();

            return result;
        }

        public virtual async Task<int> CreateAsync(T request)
        {
            var id = await _dataAccessQuery.CreateQuery()
                .Create(request)
                .CreateAsync();

            return id;
        }

        public virtual async Task<int> UpdateAsync(int id, T request)
        {
            var previousRequest = await GetByIdAsync(id);

            if (previousRequest is null)
                throw new ArgumentException($"Request with id {id} could not be found", nameof(previousRequest));

            await _dataAccessQuery.UpdateQuery()
                .Update(request)
                .WithIdValue(id)
                .UpdateAsync();

            return id;
        }

        public virtual async Task UpdateAsync(T request)
        {
            await _dataAccessQuery.UpdateQuery()
                .Update(request)
                .UpdateAsync();
        }

        public virtual async Task<int> UpsertAsync(int id, T request)
        {
            var previousRequest = await GetByIdAsync(id);

            if (previousRequest is null)
                throw new ArgumentException($"Request with id {id} could not be found", nameof(previousRequest));

            await _dataAccessQuery.UpdateQuery()
                .Update(request)
                .WithIdValue(id)
                .IgnoreNullValues(true)
                .WithUpsert(true)
                .UpdateAsync();

            return id;
        }

        public virtual async Task UpsertAsync(T request)
        {
            await _dataAccessQuery.UpdateQuery()
                .Update(request)
                .IgnoreNullValues(true)
                .WithUpsert(true)
                .UpdateAsync();
        }

        public virtual async Task DeleteAsync(T request)
        {
            await _dataAccessQuery.DeleteQuery<T>(request)
                .DeleteAsync();
        }

        public virtual async Task DeleteAsync(int id, T request)
        {
            var previousRequest = await GetByIdAsync(id);

            if (previousRequest is null)
                throw new ArgumentException($"Request with id {id} could not be found", nameof(previousRequest));

            await _dataAccessQuery.DeleteQuery<T>(request)
                .WithIdValue(id)
                .DeleteAsync();
        }

        public virtual IDataSelectQueryInner NewListQuery()
        {
            var result =
                _dataAccessQuery
                    .SelectQuery<T>();

            return result;
        }
    }
}