namespace MMS.Core.Queries
{
    public class PagedRequest : IRequest
    {
        private const int MaxPageSize = 100;

        private int _pageNumber = 1;
        private int _pageSize = 20;

        public int PageNumber
        {
            get => _pageNumber;
            set
            {
                if(value == _pageNumber)
                {
                    return;
                }

                _pageNumber = value == 0 ? 1 : value;
            }
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
        }
    }
}
