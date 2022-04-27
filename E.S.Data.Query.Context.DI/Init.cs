using E.S.Data.Query.Context.Interfaces;
using E.S.Data.Query.Context.Queries;
using E.S.Data.Query.Context.Services;
using Microsoft.Extensions.DependencyInjection;

namespace E.S.Data.Query.Context.DI
{
    public static class Init
    {
        public static void AddDataQueryContext(
            this IServiceCollection services)
        {
            services.AddTransient(typeof(IDataSelectQuery), typeof(DataSelectQuery));
            services.AddTransient(typeof(IDataCreateQuery), typeof(DataCreateQuery));
            services.AddTransient(typeof(IDataUpdateQuery), typeof(DataUpdateQuery));
            services.AddTransient(typeof(IDataDeleteQuery), typeof(DataDeleteQuery));

            services.AddScoped(typeof(IRepositoryService<>), typeof(RepositoryService<>));
        }
    }
}