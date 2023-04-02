using System.Collections.Generic;
using System.Threading.Tasks;

namespace E.S.Data.Query.Context.Interfaces
{
    public interface IRepositoryService<T> where T : class, new()
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetByCodeAsync(string code);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> CreateAsync(T request);
        Task<int> UpdateAsync(int id, T request);
        Task UpdateAsync(T request);
        Task<int> UpsertAsync(int id, T request);
        Task UpsertAsync(T request);
        Task DeleteAsync(T request);
        Task DeleteAsync(int id, T request);
        IDataSelectQueryInner NewListQuery();
    }
}