using System;

namespace Eos.Abstracts.Models.Dto
{
    public class ItemDto
    {
        public Guid ItemId { get; set; }
        public Guid? ParentId { get; set; }
        public string Title { get; set; }
        
        /// <summary>
        /// Значение отклонения
        /// </summary>
        public int Value { get; set; }

    }
}