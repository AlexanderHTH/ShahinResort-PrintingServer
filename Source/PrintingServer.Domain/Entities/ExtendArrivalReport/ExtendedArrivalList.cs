namespace PrintingServer.Domain.Entities;
public partial class ExtendedArrivalList
{
    public Guid Id { get; set; }
    public string ListDate { get; set; }
    public string CreatedBy { get; set; }
    public List<Accoumodation>? TheList { get; set; }
    public ExtendedArrivalList(Guid id, string listDate, string createdBy)
    {
        Id = id;
        ListDate = listDate;
        CreatedBy = createdBy;
    }
}
