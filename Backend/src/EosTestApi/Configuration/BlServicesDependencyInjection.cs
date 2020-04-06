using Eos.Abstracts.Bl;
using Eos.Bl;
using Microsoft.Extensions.DependencyInjection;

namespace EosTestApi.Configuration
{
    public static class BlServicesDependencyInjection
    {
        public static void AddBlServices(this IServiceCollection services)
        {
            services.AddTransient<IItemService, ItemService>();
        }
    }
}