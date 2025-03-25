namespace PrintingServer.Domain.Common;

public interface IPagedResult<TDTO> where TDTO : class
{
    IEnumerable<TDTO> Items { get; set; }
    int ItemsFrom { get; set; }
    int ItemsTo { get; set; }
    int TotalItemCount { get; set; }
    int TotalPages { get; set; }
}
