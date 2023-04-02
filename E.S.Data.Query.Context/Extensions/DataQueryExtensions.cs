using E.S.Data.Query.Context.Interfaces;
using E.S.Data.Query.Context.Services;
using E.S.Data.Query.DataAccess.Interfaces;

namespace E.S.Data.Query.Context.Extensions;

public static class DataQueryExtensions
{
    public static IRepositoryService<T> RepositoryService<T>(this IDataAccessQuery dataAccessQuery)
        where T : class, new()
    {
        return new RepositoryService<T>(dataAccessQuery);
    }
}