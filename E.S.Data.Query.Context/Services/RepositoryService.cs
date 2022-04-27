using E.S.Data.Query.Context.Interfaces;
using E.S.Data.Query.DataAccess.Interfaces;

namespace E.S.Data.Query.Context.Services
{
    public class RepositoryService<T> : RepositoryServiceBase<T>, IRepositoryService<T> where T : class, new()
    {
        public RepositoryService(IDataAccessQuery dataAccessQuery) : base(dataAccessQuery)
        {
        }
    }
}