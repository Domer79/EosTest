using System;
using System.Threading.Tasks;
using Eos.Abstracts.Entities;
using Eos.Abstracts.Models;
using Eos.Abstracts.Models.Dto;
using Eos.Abstracts.Models.Pages;

namespace Eos.Abstracts.Bl
{
    public interface IItemService
    {
        Task<Item> Get(Guid itemId);
        Task<Item> Add(Item item, Item parent);
        Task Update(Item item);
        Task Delete(Item item);
        Task Delete(Guid itemId);
        Task<ItemPage> GetChildItems(Pager pager, Guid parentId); 
        Task InitialFilling();
        Task<ItemPage> GetParents(Pager pager);
        Task<ItemPage> GetCteChildItems(Pager pager, Guid parentId);
    }
}