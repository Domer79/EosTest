using System;
using System.Collections.Generic;
using System.Text;

namespace Eos.Abstracts.Entities
{
    public class Item
    {
        public Item()
        {
            ItemId = Guid.NewGuid();
        }

        public Guid ItemId { get; set; }
        public Guid? ParentId { get; set; }
        public string Title { get; set; }
        
        /// <summary>
        /// Значение отклонения
        /// </summary>
        public int Value { get; set; }

        public Item Parent { get; set; }
        public HashSet<Item> OwnerItems { get; set; }

        public static Item Create(Guid? parentId, string title, int value)
        {
            return new Item()
            {
                ParentId = parentId,
                Title = title,
                Value = value,
            };
        }
    }
}
