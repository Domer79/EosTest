using System;
using System.Data;
using Eos.Abstracts.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Eos.Data.Dapper.Common
{
    public class SqlConnectionFactory: IConnectionFactory
    {
        private readonly IConfiguration _configuration;
        private string ConnectionString => GetConnectionString();

        public SqlConnectionFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GetConnectionString()
        {
            var connectionSection = _configuration.GetSection("common");
            var connectionString = connectionSection["connectionString"];
            var providerName = connectionSection["ProviderName"];
            return connectionString;
        }

        public Func<IDbConnection> CreateConnection => GetConnection;

        private IDbConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}