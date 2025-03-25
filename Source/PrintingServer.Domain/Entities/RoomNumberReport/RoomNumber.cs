namespace PrintingServer.Domain.Entities;
public partial class RoomNumber
{
    public Guid Id { get; set; }
    public string CreatedBy { get; set; }
    public string RoomNu { get; set; }
    public RoomNumber(Guid id, string createdBy, string roomNu)
    {
        Id = id;
        CreatedBy = createdBy;
        RoomNu = roomNu;
    }
}
