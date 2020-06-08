using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Eos.Abstracts.Data;
using Eos.Abstracts.Entities;
using Eos.Abstracts.Models;
using Eos.Abstracts.Models.Pages;
using Eos.Data.EF;
using Eos.Data.EF.Query;
using Eos.Data.Misc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Eos.Data
{
    public class ItemRepository: IItemRepository
    {
        private readonly EosContext _context;

        public ItemRepository(EosContext context)
        {
            _context = context;
        }

        public Task<Item> Get(Guid itemId)
        {
            return _context.Items.FindAsync(itemId).AsTask();
        }

        public async Task<Item> Add(Item item)
        {
            var entry = _context.Items.Add(item);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            return entry.Entity;
        }

        public Task Update(Item item)
        {
            var entry = _context.Items.Add(item);
            entry.State = EntityState.Modified;

            return _context.SaveChangesAsync();
        }

        public Task Delete(Item item)
        {
            var entry = _context.Items.Add(item);
            entry.State = EntityState.Deleted;

            return _context.SaveChangesAsync();
        }

        public Task BulkInsert(Item[] items)
        {
            _context.Items.AddRange(items);
            return _context.SaveChangesAsync();
        }

        public async Task<ItemPage> GetChilds(Pager pager, Guid parentId)
        {
            var query = from item in _context.Items
                join gi in _context.GlobalItems
                    on item.ItemId equals gi.ItemId
                    where gi.ParentId == parentId
                select item;

            query = query
                .Union(_context.Items.Where(_ => _.ItemId == parentId))
                .OrderBy(_ => _.Title);

            var items = await query.Page(pager).ToArrayAsync();
            var totalCount = await query.CountAsync();
            var maxValue = await query.MaxAsync(_ => _.Value);

            return new ItemPage()
            {
                Items = items,
                TotalCount = totalCount,
                MaxValue = maxValue
            };
        }

        public async Task<ItemPage> GetCteChilds(Pager pager, Guid parentId)
        {
            const string pageSql = @"
with cte (ItemId, Title, Value, ParentId) as
(
	select ItemId, Title, Value, ParentId from Items where ParentId = @parentId
	union all
	select i.ItemId, i.Title, i.Value, i.ParentId from Items i 
	inner join cte on i.ParentId = cte.ItemId
)
select * from
(
select ItemId, Title, Value, ParentId from cte
union all
select ItemId, Title, Value, ParentId from Items where ItemId = @parentId
)s
ORDER BY Title
OFFSET @from ROWS FETCH NEXT @size ROWS ONLY;
";
            const string countSql = @"
with cte (ItemId, Title, Value, ParentId) as
(
	select ItemId, Title, Value, ParentId from Items where ParentId = @parentId
	union all
	select i.ItemId, i.Title, i.Value, i.ParentId from Items i 
	inner join cte on i.ParentId = cte.ItemId
)
select count(1) from
(
select ItemId, Title, Value, ParentId from cte
union all
select ItemId, Title, Value, ParentId from Items where ItemId = @parentId
)s;
";
            const string maxSql = @"
with cte (ItemId, Title, Value, ParentId) as
(
	select ItemId, Title, Value, ParentId from Items where ParentId = @parentId
	union all
	select i.ItemId, i.Title, i.Value, i.ParentId from Items i 
	inner join cte on i.ParentId = cte.ItemId
)
select max(Value) from
(
select ItemId, Title, Value, ParentId from cte
union all
select ItemId, Title, Value, ParentId from Items where ItemId = @parentId
)s;
";
            
            var from = (pager.Page - 1) * pager.ItemsPerPage;
            var size = pager.ItemsPerPage;
            var items = await _context.Items.FromSqlRaw(pageSql, 
                new SqlParameter("parentId", parentId), 
                new SqlParameter("from", from), 
                new SqlParameter("size", size)).ToArrayAsync();
            var totalCount = await _context.ExecuteScalar<int>(countSql, new SqlParameter("parentId", parentId));
            var maxValue = await _context.ExecuteScalar<int>(maxSql, new SqlParameter("parentId", parentId));

            return new ItemPage()
            {
                Items = items,
                TotalCount = totalCount,
                MaxValue = maxValue
            };
        }

        public async Task AllDelete()
        {
            Item[] items = await _context.Items.Take(100).ToArrayAsync();
            while (items.Any())
            {
                _context.Items.RemoveRange(items);
                await _context.SaveChangesAsync();
            }

            // return _context.Database.ExecuteSqlRawAsync("delete from Items");
        }

        public async Task<Item[]> GreaterTitle(string sourceTitle)
        {
            // var items = await _context.Items.Where(_ => _context.EFFunctions.StringCompare(_.Title, "A")).ToListAsync();
            var items = await _context.Items.Where(_ => _.Title.Compare("A")).ToArrayAsync();

            return items;
        }

        public async Task<ItemPage> GetParents(Pager pager)
        {
            var parentQuery = _context.Items
                .Select(_ => _.ParentId)
                .GroupBy(_ => _)
                .Where(_ => _.Key != null)
                .Select(_ => new {ParentId = _.Key});

            var itemQuery = _context.Items.Join(parentQuery, item => item.ItemId, parentId => parentId.ParentId, 
                (i,p) => i);
            
            var items = await itemQuery
                .OrderBy(_ => _.Title)
                .Page(pager)
                .ToArrayAsync();
            var totalCount = await itemQuery.CountAsync();
            
            return new ItemPage()
            {
                Items = items,
                TotalCount = totalCount
            };
        }
    }
}