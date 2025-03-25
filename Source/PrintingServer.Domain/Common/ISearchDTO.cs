namespace PrintingServer.Domain.Common;

public interface ISearchDTO<T> where T : class
{
    List<SearchCondition>? Conditions { get; set; }
    List<ResultSort>? Sorts { get; set; }
    int PageNumber { get; set; }
    int PageSize { get; set; }
}
