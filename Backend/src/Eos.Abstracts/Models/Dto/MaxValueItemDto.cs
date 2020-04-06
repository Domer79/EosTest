using System;

namespace Eos.Abstracts.Models.Dto
{
    public class MaxValueItemDto
    {
        public Guid ItemId { get; set; }
        public string Title { get; set; }
        public int Value { get; set; }
    }
}