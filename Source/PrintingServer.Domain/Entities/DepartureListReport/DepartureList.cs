namespace PrintingServer.Domain.Entities;
public partial class DepartureList
{
    public Guid Id { get; set; }
    public string DepartureDate { get; set; }
    public string CreatedBy { get; set; }
    public List<DepartureListItem>? TheList { get; set; }
    public DepartureList(Guid id, string departureDate, string createdBy)
    {
        Id = id;
        DepartureDate = departureDate;
        CreatedBy = createdBy;
    }
}
