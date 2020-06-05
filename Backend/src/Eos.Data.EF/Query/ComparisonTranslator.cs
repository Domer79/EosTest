// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Eos.Data.EF.Query
{
    /// <summary>
    /// За основу взято
    /// https://github.com/dotnet/efcore/blob/master/src/EFCore.Relational/Query/Internal/ComparisonTranslator.cs
    /// </summary>
    public class ComparisonTranslator : IMethodCallTranslator
    {
        
        public virtual SqlExpression Translate(SqlExpression instance, MethodInfo method, IReadOnlyList<SqlExpression> arguments)
        {
            if (method.ReturnType == typeof(bool))
            {
                SqlExpression left = null;
                SqlExpression right = null;
                if (method.Name == nameof(Text.Compare)
                    && arguments.Count == 2
                    && arguments[0].Type.UnwrapNullableType() == arguments[1].Type.UnwrapNullableType())
                {
                    left = arguments[0];
                    right = arguments[1];
                }
                else if (method.Name == nameof(string.CompareTo)
                    && arguments.Count == 1
                    && instance != null
                    && instance.Type.UnwrapNullableType() == arguments[0].Type.UnwrapNullableType())
                {
                    left = instance;
                    right = arguments[0];
                }

                if (left != null
                    && right != null)
                {
                    return new SqlBinaryExpression(ExpressionType.GreaterThan, left, right, method.ReturnType, null);
                }
            }

            return null;
        }
    }
}
