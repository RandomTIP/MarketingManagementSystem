using Microsoft.EntityFrameworkCore;

namespace MMS.Core.Pagination;

public class PagedList<T> : PageParams
{
    public List<T> List { get; }

    public PagedList(int pageNumber, int pageSize, int totalCount, IEnumerable<T> list) : base(pageNumber, pageSize, totalCount)
    {
        List = list.Skip(PageIndex * PageSize).Take(PageSize).ToList();
    }

    public PagedList() : base(0, 0, 0)
    {
        List = new List<T>();
    }

    private PagedList(List<T> list, int pageNumber, int pageSize, int totalCount) : base(pageNumber, pageSize, totalCount)
    {
        List = list;
    }

    public static async Task<PagedList<TObj>> CreateAsync<TObj>(IQueryable<TObj> source, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
    {
        var totalCount = await source.CountAsync(cancellationToken);

        var list = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);

        return new PagedList<TObj>(list, pageNumber, pageSize, totalCount);
    }
}