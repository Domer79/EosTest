using System;
using System.Linq;
using System.Threading.Tasks;
using Eos.Abstracts.Data;
using Eos.Abstracts.Entities;
using Eos.Data.EF;
using Microsoft.EntityFrameworkCore;

namespace Eos.Data
{
    public class GlobalItemRepository: IGlobalItemRepository
    {
        private readonly EosContext _context;

        public GlobalItemRepository(EosContext context)
        {
            _context = context;
        }
        
        public Task<GlobalItem> Get(Guid parentId, Guid itemId)
        {
            return _context.GlobalItems.SingleOrDefaultAsync(_ => _.ParentId == parentId && _.ItemId == itemId);
        }

        public Task<GlobalItem[]> GetGlobalParentItems(Guid parentId)
        {
            return _context.GlobalItems.Where(_ => _.ParentId == parentId).ToArrayAsync();
        }

        public async Task<GlobalItem> Add(GlobalItem item)
        {
            var entry = _context.GlobalItems.Add(item);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            return entry.Entity;
        }

        public Task Delete(GlobalItem item)
        {
            var entry = _context.GlobalItems.Add(item);
            entry.State = EntityState.Deleted;

            return _context.SaveChangesAsync();
        }

        public Task BulkInsert(GlobalItem[] items)
        {
            _context.GlobalItems.AddRange(items);
            return _context.SaveChangesAsync();
        }

        public async Task AllDelete()
        {
            GlobalItem[] items = await _context.GlobalItems.Take(100).ToArrayAsync();
            while (items.Any())
            {
                _context.GlobalItems.RemoveRange(items);
                await _context.SaveChangesAsync();
            }
            // return _context.Database.ExecuteSqlRawAsync("delete from GlobalItems");
        }
    }
}