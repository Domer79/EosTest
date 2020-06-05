using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Eos.Data.EF.Query
{
    public static class SqlServerDbContextOptionsBuilderExtensions
    {
        public static SqlServerDbContextOptionsBuilder AddStringCompareSupport(
            this SqlServerDbContextOptionsBuilder sqlServerOptionsBuilder)
        {
            var infrastructure = (IRelationalDbContextOptionsBuilderInfrastructure)
                sqlServerOptionsBuilder;
            var builder = (IDbContextOptionsBuilderInfrastructure)
                infrastructure.OptionsBuilder;

            // if the extension is registered already then we keep it 
            // otherwise we create a new one
            var extension = infrastructure.OptionsBuilder.Options
                                .FindExtension<SqlServerDbContextOptionsExtension>()
                            ?? new SqlServerDbContextOptionsExtension();
            builder.AddOrUpdateExtension(extension);

            return sqlServerOptionsBuilder;
        }
    }
}