using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;

namespace Eos.Data.EF.Query
{
    public class SqlServerDbContextOptionsExtension: IDbContextOptionsExtension
    {
        public SqlServerDbContextOptionsExtension()
        {
            Info = new EosSqlServerDbContextOptionsExtensionInfo(this);
        }

        public void ApplyServices(IServiceCollection services)
        {
            services.AddSingleton<IMethodCallTranslatorPlugin, SqlServerMethodCallTranslatorPlugin>();
        }

        public void Validate(IDbContextOptions options)
        {
        }

        public DbContextOptionsExtensionInfo Info { get; }
    }
}