using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Eos.Abstracts.Data;
using Eos.Abstracts.Entities;
using Eos.Abstracts.Models;

namespace Eos.Data.Dapper
{
    public class ItemRepository: IItemRepository
    {
        private readonly ICommonDb _commonDb;

        public ItemRepository(ICommonDb commonDb)
        {
            _commonDb = commonDb;
        }
        
        public Task<Item> Get(Guid itemId)
        {
            return _commonDb.QuerySingleOrDefaultAsync<Item>("select * from Items where ItemId = @itemId", new {itemId});
        }

        public async Task<Item[]> GetItems(Guid parentId, Pager pager)
        {
            var offsetRowNumber = pager.Number * pager.Size - pager.Size;
            var items = await _commonDb.QueryAsync<Item>("select * from Items where ParentId = @parentId " +
                "order by ItemId offset @offsetRowNumber fetch next @size rows only", new { offsetRowNumber, pager.Size });

            return items.ToArray();
        }

        public async Task<Item> Add(Item item)
        {
            const string query = @"insert into Items(ItemId, ParentId, Title, Value) values(@itemId, @parentId, @title, @value)";
            await _commonDb.ExecuteScalarAsync<int>(query, item);
            return item;
        }

        public Task Update(Item item)
        {
            const string query = "@update Items set Title = @title, Value = @value where ItemId = @itemId";
            return _commonDb.ExecuteNonQueryAsync(query, item);
        }

        public Task Delete(Item item)
        {
            const string query = "delete from Items where ItemId = @itemId";
            return _commonDb.ExecuteNonQueryAsync(query, new {item.ItemId});
        }

        public async Task BulkInsert(Item[] items)
        {
            foreach (var item in items)
            {
                await Add(item);
            }
        }
    }
}