using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Query;

namespace Eos.Data.EF.Query
{
    public class SqlServerMethodCallTranslatorPlugin : IMethodCallTranslatorPlugin
    {
        public IEnumerable<IMethodCallTranslator> Translators { get; }

        public SqlServerMethodCallTranslatorPlugin()
        {
            Translators = new List<IMethodCallTranslator> 
            {
                new ComparisonTranslator()
            };
        }
    }
}