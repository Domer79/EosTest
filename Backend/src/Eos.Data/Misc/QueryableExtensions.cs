using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Eos.Abstracts.Models;
using Microsoft.EntityFrameworkCore;

namespace Eos.Data.Misc
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Page<T>(this IQueryable<T> query, Pager pager)
        {
            return query.Skip((pager.Page - 1) * pager.ItemsPerPage).Take(pager.ItemsPerPage);
        }

        public static async Task<T> ExecuteScalar<T>(this DbContext context, string query, params object[] parameters)
        {
            DbConnection connection = context.Database.GetDbConnection();
            try
            {
                await using var cmd = connection.CreateCommand();
                cmd.CommandText = query;

                foreach (var parameter in parameters)
                {
                    cmd.Parameters.Add(parameter);
                }

                if (connection.State.Equals(ConnectionState.Closed)) 
                    connection.Open(); 

                return (T) await cmd.ExecuteScalarAsync();
            }
            finally
            {
                if (connection.State.Equals(ConnectionState.Open)) 
                    connection.Close(); 
            }
        }
    }
}