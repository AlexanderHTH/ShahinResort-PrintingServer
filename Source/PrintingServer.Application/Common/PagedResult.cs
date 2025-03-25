using PrintingServer.Domain.Common;

namespace PrintingServer.Application.Common;

public class PagedResult<TDTO> : IPagedResult<TDTO> where TDTO : class
{
    public IEnumerable<TDTO> Items { get; set; }
    public int TotalPages { get; set; }
    public int TotalItemCount { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }
    public PagedResult(IEnumerable<TDTO>? items, int totalCount, int pageSize, int pageNumber)
    {
        Items = items ?? [];
        TotalItemCount = totalCount;
        TotalPages = pageSize <= 0 ? 1 : (int)Math.Ceiling(totalCount / (double)pageSize);
        ItemsFrom = pageSize * (pageNumber - 1) + 1;
        ItemsTo = TotalItemCount < pageSize ? TotalItemCount : ItemsFrom + pageSize - 1;
    }

    public PagedResult(IEnumerable<TDTO>? items, int totalCount,ISearchDTO<TDTO> searchDTO)
    {
        Items = items ?? [];
        TotalItemCount = totalCount;
        TotalPages = searchDTO.PageSize <= 0 ? 1 : (int)Math.Ceiling(totalCount / (double)searchDTO.PageSize);
        ItemsFrom = searchDTO.PageSize * (searchDTO.PageNumber - 1) + 1;
        ItemsTo = TotalItemCount < searchDTO.PageSize ? TotalItemCount : ItemsFrom + searchDTO.PageSize - 1;
    }

    public PagedResult()
    {
        Items = [];
        TotalPages = 0;
        TotalItemCount = 0;
        ItemsFrom = 0;
        ItemsTo = 0;
    }
}
