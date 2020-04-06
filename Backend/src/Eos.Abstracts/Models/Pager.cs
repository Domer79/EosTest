namespace Eos.Abstracts.Models
{
    public class Pager
    {
        public int Page { get; set; }
        public int ItemsPerPage { get; set; }
        public string Query { get; set; }
    }
}