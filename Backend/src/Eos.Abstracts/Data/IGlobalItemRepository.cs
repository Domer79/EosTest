using System;
using System.Threading.Tasks;
using Eos.Abstracts.Entities;

namespace Eos.Abstracts.Data
{
    public interface IGlobalItemRepository
    {
        Task<GlobalItem> Get(Guid parentId, Guid itemId);
        Task<GlobalItem[]> GetGlobalParentItems(Guid parentId);
        Task<GlobalItem> Add(GlobalItem item);
        Task Delete(GlobalItem item);
        Task BulkInsert(GlobalItem[] items);
        Task AllDelete();
    }
}