using System.Collections.Generic;

namespace Ebaysharp.Entities
{
    public class EbayList<T, F> : List<T> where F : EbayFilter
    {
        private F _filter { get; set; }

        public EbayList(F filter, List<T> records)
        {
            _filter = filter;
            if (records != null)
                AddRange(records);
        }

        public F GetNextPageFilter()
        {
            return _filter;
        }

        public bool HasNextPage()
        {
            return _filter.NextPage != null;
        }
    }
}
