using System;

namespace Eos.Data.EF.Query
{
    public static class Text
    {
        public static bool Compare(this string strA, string strB) 
            => throw new InvalidOperationException(
                "This method is for use with Entity Framework Core only and has no in-memory implementation.");
    }
}