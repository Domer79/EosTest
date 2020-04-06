using Eos.Abstracts.Models.Dto;

namespace Eos.Abstracts.Models.Pages
{
    public class ItemDtoPage
    {
        public ItemDto[] Items { get; set; }
        public int TotalCount { get; set; }
        public int MaxValue { get; set; }
    }
}