using System;
using Microsoft.EntityFrameworkCore;

namespace Eos.Data.EF.Query
{
    public static class EFExtensions
    {
        public static Type UnwrapNullableType(this Type type)
            => Nullable.GetUnderlyingType(type) ?? type;
    }
}