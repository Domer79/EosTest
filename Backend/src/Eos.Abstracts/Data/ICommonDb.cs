using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Eos.Abstracts.Models;

namespace Eos.Abstracts.Data
{
    public interface ICommonDb
    {
        Task<IEnumerable<T>> QueryAsync<T>(string query, object parameters = null);
        Task<T> QueryFirstAsync<T>(string query, object parameters = null);
        Task<T> QueryFirstOrDefaultAsync<T>(string query, object parameters = null);
        Task<T> QuerySingleAsync<T>(string query, object parameters = null);
        Task<T> QuerySingleOrDefaultAsync<T>(string query, object parameters = null);
        Task<T> ExecuteScalarAsync<T>(string query, object parameters = null);
        Task<int> ExecuteNonQueryAsync(string query, object parameters = null);
        Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(string query, Func<T1, T2, TReturn> p, object parameters = null, string splitOn = "Id");
    }
}