using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;

namespace Eos.Data.EF.Postgres.Query
{
    public class NpgsqlDbContextOptionsExtension: IDbContextOptionsExtension
    {
        public NpgsqlDbContextOptionsExtension()
        {
            Info = new EosNpgsqlDbContextOptionsExtensionInfo(this);
        }

        public void ApplyServices(IServiceCollection services)
        {
            services.AddSingleton<IMethodCallTranslatorPlugin, NpgsqlMethodCallTranslatorPlugin>();
        }

        public void Validate(IDbContextOptions options)
        {
        }

        public DbContextOptionsExtensionInfo Info { get; }
    }
}