using Eos.Abstracts.Data;
using Eos.Data;
using Microsoft.Extensions.DependencyInjection;

namespace EosTestApi.Configuration
{
    public static class RepositoryDependencyInjection
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IItemRepository, ItemRepository>();
            services.AddTransient<IGlobalItemRepository, GlobalItemRepository>();
        }
    }
}
