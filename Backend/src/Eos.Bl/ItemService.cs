using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eos.Abstracts.Bl;
using Eos.Abstracts.Data;
using Eos.Abstracts.Entities;
using Eos.Abstracts.Models;
using Eos.Abstracts.Models.Dto;
using Eos.Abstracts.Models.Pages;

namespace Eos.Bl
{
    public class ItemService: IItemService
    {
        private readonly IItemRepository _repo;
        private readonly IGlobalItemRepository _globalRepository;
        private readonly List<Item> _items = new List<Item>();
        private readonly List<GlobalItem> _globalItems = new List<GlobalItem>();
        private readonly Random _rand = new Random();

        public ItemService(IItemRepository repo, IGlobalItemRepository globalRepository)
        {
            _repo = repo;
            _globalRepository = globalRepository;
        }
        
        public Task<Item> Get(Guid itemId)
        {
            return _repo.Get(itemId);
        }

        public Task<Item> Add(Item item, Item parent)
        {
            return _repo.Add(item);
        }

        public Task Update(Item item)
        {
            return _repo.Update(item);
        }

        public Task Delete(Item item)
        {
            return _repo.Delete(item);
        }

        public async Task Delete(Guid itemId)
        {
            var item = await Get(itemId);
            await _repo.Delete(item);
        }

        public async Task<ItemPage> GetChildItems(Pager pager, Guid parentId)
        {
            var childItemPage = await _repo.GetChilds(pager, parentId);
            return childItemPage;
        }

        public async Task<ItemPage> GetCteChildItems(Pager pager, Guid parentId)
        {
            var childItemPage = await _repo.GetCteChilds(pager, parentId);
            return childItemPage;
        }
        
        public async Task InitialFilling()
        {
            Glossary.Reset();
            await AllDelete();
            do
            {
                //Console.WriteLine(Glossary.TitleCurrent);
            } while (FillItem(_rand.Next(1, 20), 100, null));
            
            Console.WriteLine(_items.Count);
            Console.WriteLine(_globalItems.Count);

            var splittedItems = SplitList(_items, 1000);
            foreach (var list in splittedItems)
            {
                await _repo.BulkInsert(list.ToArray());
            }

            var splittedGlobalItems = SplitList(_globalItems, 1000);
            foreach (var list in splittedGlobalItems)
            {
                await _globalRepository.BulkInsert(list.ToArray());
            }
        }

        public Task<ItemPage> GetParents(Pager pager)
        {
            return _repo.GetParents(pager);
        }

        private bool FillItem(int depth, int size, Item parent)
        {
            try
            {
                if (depth == 0)
                    return true;

                for (var i = 0; i < size; i++)
                {
                    var item = Item.Create(parent?.ItemId, Glossary.GetTitle(), _rand.Next(30));
                    _items.Add(item);
                    AddToMainParent(parent, item, 0);

                    if (!FillItem(depth - 1, _rand.Next(1, 5), item))
                        return false;
                }

                return true;
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        private void AddToMainParent(Item parent, Item item, int index)
        {
            if (parent == null)
                return;

            var mainParent = _items.SingleOrDefault(_ => _.ItemId == parent.ParentId);
            AddToMainParent(mainParent, item, ++index);
            
            _globalItems.Add(GlobalItem.Create(parent.ItemId, item.ItemId, index));
        }
        
        private async Task AllDelete()
        {
            await _globalRepository.AllDelete();
            await _repo.AllDelete();
        }
        
        private static List<List<T>> SplitList<T>(List<T> source, int nSize = 30)
        {
            var list = new List<List<T>>();

            for (int i = 0; i < source.Count; i += nSize)
            {
                list.Add(source.GetRange(i, Math.Min(nSize, source.Count - i)));
            }

            return list;
        }
    }
}