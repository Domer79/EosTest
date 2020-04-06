using System;
using System.Collections.Generic;

namespace Eos.Abstracts.Entities
{
    /// <summary>
    /// Сущность, определяющая всех потомков главного родителя
    /// </summary>
    public class GlobalItem
    {
        public Guid ParentId { get; set; }
        public Guid ItemId { get; set; }
        
        public int ParentIndex { get; set; }

        public static GlobalItem Create(Guid mainParentId, Guid itemId, int index)
        {
            return new GlobalItem()
            {
                ParentId = mainParentId,
                ItemId = itemId,
                ParentIndex = index
            };
        }
    }
}