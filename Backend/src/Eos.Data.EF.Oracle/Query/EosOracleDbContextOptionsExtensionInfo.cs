using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Eos.Data.EF.Oracle.Query
{
    public class EosOracleDbContextOptionsExtensionInfo: DbContextOptionsExtensionInfo
    {
        private readonly IDbContextOptionsExtension _extension;

        public EosOracleDbContextOptionsExtensionInfo(IDbContextOptionsExtension extension) : base(extension)
        {
            _extension = extension;
        }

        public override long GetServiceProviderHashCode()
        {
            return _extension.GetHashCode();
        }

        public override void PopulateDebugInfo(IDictionary<string, string> debugInfo)
        {
            
        }

        public override bool IsDatabaseProvider { get; }
        public override string LogFragment { get; } = "'StringCompareSupport'=true";
    }
}