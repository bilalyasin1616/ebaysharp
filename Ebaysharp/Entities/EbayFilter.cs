namespace Ebaysharp.Entities
{
    public class EbayFilter
    {
        public int Limit { get; set; }
        public string NextPage { get; set; }

        public EbayFilter(int limit, string href = null)
        {
            Limit = limit;
            NextPage = href;
        }
    }

}
