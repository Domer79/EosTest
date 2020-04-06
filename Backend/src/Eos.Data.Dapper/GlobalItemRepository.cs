using System;
using System.Linq;
using System.Threading.Tasks;
using Eos.Abstracts.Data;
using Eos.Abstracts.Entities;

namespace Eos.Data.Dapper
{
    public class GlobalItemRepository: IGlobalItemRepository
    {
        private readonly ICommonDb _commonDb;

        public GlobalItemRepository(ICommonDb commonDb)
        {
            _commonDb = commonDb;
        }
        
        public Task<GlobalItem> Get(Guid parentId, Guid itemId)
        {
            const string query = "select * from GlobalItem where ParentId = @parentId and ItemId = @itemId";
            return _commonDb.QuerySingleOrDefaultAsync<GlobalItem>(query, new {parentId, itemId});
        }

        public async Task<GlobalItem[]> GetGlobalParentItems(Guid parentId)
        {
            const string query = "select * from GlobalItem where ParentId = @parentId";
            var items = await _commonDb.QueryAsync<GlobalItem>(query, new {parentId});
            return items.ToArray();
        }

        public async Task<GlobalItem> Add(GlobalItem item)
        {
            const string query = "insert into GlobalItem(ParentId, ItemId) values(@parentId, @itemId)";
            await _commonDb.ExecuteNonQueryAsync(query, item);
            return item;
        }

        public Task Delete(GlobalItem item)
        {
            const string query = "delete from GlobalItem where ParentId = @parentId and ItemId = @itemId";
            return _commonDb.ExecuteNonQueryAsync(query, item);
        }

        public async Task BulkInsert(GlobalItem[] items)
        {
            foreach (var item in items)
            {
                await Add(item);
            }
        }
    }
}