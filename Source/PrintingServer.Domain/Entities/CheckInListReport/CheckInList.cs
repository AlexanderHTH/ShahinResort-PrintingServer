namespace PrintingServer.Domain.Entities;
public partial class CheckInList
{
    public Guid Id { get; set; }
    public string ListDate { get; set; }
    public string CreatedBy { get; set; }
    public List<CheckInListItem>? TheList { get; set; }
    public CheckInList(Guid id, string listDate, string createdBy)
    {
        Id = id;
        ListDate = listDate;
        CreatedBy = createdBy;
    }
}
