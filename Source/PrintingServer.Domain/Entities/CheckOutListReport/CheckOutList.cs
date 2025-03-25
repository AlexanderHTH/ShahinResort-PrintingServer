namespace PrintingServer.Domain.Entities;
public partial class CheckOutList
{
    public Guid Id { get; set; }
    public string ListDate { get; set; }
    public string CreatedBy { get; set; }
    public List<CheckOutListItem>? TheList { get; set; }
    public CheckOutList(Guid id, string listDate, string createdBy)
    {
        Id = id;
        ListDate = listDate;
        CreatedBy = createdBy;
    }
}
