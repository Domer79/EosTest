using System;
using System.Data;

namespace Eos.Abstracts.Data
{
    public interface IConnectionFactory
    {
        Func<IDbConnection> CreateConnection { get; }
    }
}