namespace MMS.Core.Pagination
{
    public class PageParams
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public int TotalCount { get; }

        public int PageIndex => PageNumber - 1;

        public int TotalPages
        {
            get
            {
                var total = TotalCount / PageSize;

                var module = TotalCount % PageSize;

                return module > 0 ? total + 1 : total;
            }
        }

        public bool HasPreviousPage => PageIndex > 0;

        public bool HasNextPage => PageNumber < TotalPages;

        public int FirstItemIndex => (PageIndex * PageSize) + 1;

        public int LastItemIndex => Math.Min(TotalCount, ((PageIndex * PageSize) + PageSize));

        public bool IsFirstPage => PageNumber <= 1;

        public bool IsLastPage => PageNumber >= TotalPages;

        public PageParams(int pageNumber, int pageSize, int totalCount)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
        }
    }
}
