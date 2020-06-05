using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Eos.Abstracts.Entities;
using Eos.Data.EF.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;

namespace Eos.Data.EF
{
    public class EosContext: DbContext
    {
        public EosContext()
        {
        }

        public EosContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<GlobalItem> GlobalItems { get; set; }

        public DbFunctions EFFunctions => Microsoft.EntityFrameworkCore.EF.Functions;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                var changeTrack = false;
                //для ручной отладки
                if (changeTrack)
                {
                    ChangeTracker.DetectChanges();
                    var entries = ChangeTracker
                        .Entries()
                        .Where(_ => _.State == EntityState.Added ||
                                    _.State == EntityState.Modified ||
                                    _.State == EntityState.Deleted)
                        .ToArray();
                    foreach (var entry in entries)
                    {
                        Debug.WriteLine($"{entry.Entity}={entry.State}");
                    }
                }
                await base.SaveChangesAsync(true, cancellationToken);
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
                throw;
            }

            return 0;
        }
    }
}
