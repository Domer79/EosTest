using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Eos.Abstracts.Data;
using Eos.Abstracts.Models;

namespace Eos.Data.Dapper.Common
{
    public class CommonDb: ICommonDb
    {
        private readonly IConnectionFactory _connectionFactory;

        public CommonDb(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        
        public Task<IEnumerable<T>> QueryAsync<T>(string query, object parameters = null)
        {
            return _connectionFactory.CreateConnection().QueryAsync<T>(query, parameters);
        }
        
        public Task<T> ExecuteScalarAsync<T>(string query, object parameters = null)
        {
            return _connectionFactory.CreateConnection().ExecuteScalarAsync<T>(query, parameters);
        }

        public Task<int> ExecuteNonQueryAsync(string query, object parameters = null)
        {
            return _connectionFactory.CreateConnection().ExecuteAsync(query, parameters);
        }

        public Task<IEnumerable<TReturn>> QueryAsync<T1, T2, TReturn>(string query, Func<T1, T2, TReturn> p,
            object parameters = null, string splitOn = "Id")
        {
            return _connectionFactory.CreateConnection().QueryAsync<T1, T2, TReturn>(query, p, parameters, splitOn:splitOn);
        }

        public T QueryFirst<T>(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.QueryFirst<T>(query, parameters);
            }
        }

        public T QueryFirstOrDefault<T>(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.QueryFirstOrDefault<T>(query, parameters);
            }
        }

        public T QuerySingle<T>(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.QuerySingle<T>(query, parameters);
            }
        }

        public T QuerySingleOrDefault<T>(string query, object parameters = null)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                return connection.QuerySingleOrDefault<T>(query, parameters);
            }
        }

        public Task<T> QueryFirstAsync<T>(string query, object parameters = null)
        {
            return _connectionFactory.CreateConnection().QueryFirstAsync<T>(query, parameters);
        }

        public Task<T> QueryFirstOrDefaultAsync<T>(string query, object parameters = null)
        {
            return _connectionFactory.CreateConnection().QueryFirstOrDefaultAsync<T>(query, parameters);
        }

        public Task<T> QuerySingleAsync<T>(string query, object parameters = null)
        {
            return _connectionFactory.CreateConnection().QuerySingleAsync<T>(query, parameters);
        }

        public Task<T> QuerySingleOrDefaultAsync<T>(string query, object parameters = null)
        {
            return _connectionFactory.CreateConnection().QuerySingleOrDefaultAsync<T>(query, parameters);
        }

        public class DateTimeOffsetTypeHandler : SqlMapper.TypeHandler<DateTimeOffset>
        {
            public override DateTimeOffset Parse(object value)
            {
                if (value is DateTimeOffset offset)
                    return offset;

                return DateTimeOffset.Parse(value.ToString());
            }

            public override void SetValue(IDbDataParameter parameter, DateTimeOffset value)
            {
                parameter.Value = value.ToString();
            }
        }
    }
}