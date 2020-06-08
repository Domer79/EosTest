using Microsoft.EntityFrameworkCore.Infrastructure;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;

namespace Eos.Data.EF.Postgres.Query
{
    public static class NpgsqlDbContextOptionsBuilderExtensions
    {
        public static NpgsqlDbContextOptionsBuilder AddStringCompareSupport(
            this NpgsqlDbContextOptionsBuilder sqlServerOptionsBuilder)
        {
            var infrastructure = (IRelationalDbContextOptionsBuilderInfrastructure)
                sqlServerOptionsBuilder;
            var builder = (IDbContextOptionsBuilderInfrastructure)
                infrastructure.OptionsBuilder;

            // if the extension is registered already then we keep it 
            // otherwise we create a new one
            var extension = infrastructure.OptionsBuilder.Options
                                .FindExtension<NpgsqlDbContextOptionsExtension>()
                            ?? new NpgsqlDbContextOptionsExtension();
            builder.AddOrUpdateExtension(extension);

            return sqlServerOptionsBuilder;
        }
    }
}