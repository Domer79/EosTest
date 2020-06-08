using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Query;

namespace Eos.Data.EF.Postgres.Query
{
    public class NpgsqlMethodCallTranslatorPlugin : IMethodCallTranslatorPlugin
    {
        public IEnumerable<IMethodCallTranslator> Translators { get; }

        public NpgsqlMethodCallTranslatorPlugin()
        {
            Translators = new List<IMethodCallTranslator> 
            {
                new ComparisonTranslator()
            };
        }
    }
}