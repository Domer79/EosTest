using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.DependencyInjection;

namespace Eos.Data.EF.Oracle.Query
{
    public class OracleDbContextOptionsExtension: IDbContextOptionsExtension
    {
        public OracleDbContextOptionsExtension()
        {
            Info = new EosOracleDbContextOptionsExtensionInfo(this);
        }

        public void ApplyServices(IServiceCollection services)
        {
            services.AddSingleton<IMethodCallTranslatorPlugin, OracleMethodCallTranslatorPlugin>();
        }

        public void Validate(IDbContextOptions options)
        {
        }

        public DbContextOptionsExtensionInfo Info { get; }
    }
}