using Eos.Abstracts.Models.Dto;

namespace Eos.Abstracts.Models.Pages
{
    public class MaxValuesItemPage
    {
        public MaxValueItemDto[] Items { get; set; }
        public int TotalCount { get; set; }
    }
}