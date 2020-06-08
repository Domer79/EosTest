using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Query;

namespace Eos.Data.EF.Oracle.Query
{
    public class OracleMethodCallTranslatorPlugin : IMethodCallTranslatorPlugin
    {
        public IEnumerable<IMethodCallTranslator> Translators { get; }

        public OracleMethodCallTranslatorPlugin()
        {
            Translators = new List<IMethodCallTranslator> 
            {
                new ComparisonTranslator()
            };
        }
    }
}