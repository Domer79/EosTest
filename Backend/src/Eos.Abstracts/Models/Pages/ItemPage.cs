using Eos.Abstracts.Entities;

namespace Eos.Abstracts.Models.Pages
{
    public class ItemPage
    {
        public Item[] Items { get; set; }
        public int TotalCount { get; set; }
        public int MaxValue { get; set; }
    }
}