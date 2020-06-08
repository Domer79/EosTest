using Microsoft.EntityFrameworkCore.Infrastructure;
using Oracle.EntityFrameworkCore.Infrastructure;

namespace Eos.Data.EF.Oracle.Query
{
    public static class OracleDbContextOptionsBuilderExtensions
    {
        public static OracleDbContextOptionsBuilder AddStringCompareSupport(
            this OracleDbContextOptionsBuilder sqlServerOptionsBuilder)
        {
            var infrastructure = (IRelationalDbContextOptionsBuilderInfrastructure)
                sqlServerOptionsBuilder;
            var builder = (IDbContextOptionsBuilderInfrastructure)
                infrastructure.OptionsBuilder;

            // if the extension is registered already then we keep it 
            // otherwise we create a new one
            var extension = infrastructure.OptionsBuilder.Options
                                .FindExtension<OracleDbContextOptionsExtension>()
                            ?? new OracleDbContextOptionsExtension();
            builder.AddOrUpdateExtension(extension);

            return sqlServerOptionsBuilder;
        }
    }
}