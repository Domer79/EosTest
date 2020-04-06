using System;
using System.Threading.Tasks;
using Eos.Abstracts.Entities;
using Eos.Abstracts.Models;
using Eos.Abstracts.Models.Pages;

namespace Eos.Abstracts.Data
{
    public interface IItemRepository
    {
        Task<Item> Get(Guid itemId);
        Task<Item> Add(Item item);
        Task Update(Item item);
        Task Delete(Item item);
        Task BulkInsert(Item[] items);
        Task<ItemPage> GetChilds(Pager pager, Guid parentId);
        Task<ItemPage> GetParents(Pager pager);
        Task<ItemPage> GetCteChilds(Pager pager, Guid parentId);
        Task AllDelete();
    }
}