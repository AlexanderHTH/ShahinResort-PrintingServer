namespace PrintingServer.Domain.Entities;
public partial class AccoumodationList
{
    public Guid Id { get; set; }
    public string ListDate { get; set; }
    public string CreatedBy { get; set; }
    public List<AccoumodationListItem>? TheList { get; set; }
    public AccoumodationList(Guid id, string listDate, string createdBy)
    {
        Id = id;
        ListDate = listDate;
        CreatedBy = createdBy;
    }

}
