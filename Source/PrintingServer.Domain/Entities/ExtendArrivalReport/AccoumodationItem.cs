namespace PrintingServer.Domain.Entities;
public partial class AccoumodationItem
{
    public Guid Id { get; set; }
    public Guid AccDtoID { get; set; }
    public string RefferanceNumber { get; set; }
    public string ReservationName { get; set; }
    public string RoomNumber { get; set; }
    public string InDate { get; set; }
    public string OutDate { get; set; }
    public AccoumodationItem(Guid id, Guid accDtoID, string refferanceNumber, string reservationName, string roomNumber, string inDate, string outDate)
    {
        Id = id;
        AccDtoID = accDtoID;
        RefferanceNumber = refferanceNumber;
        ReservationName = reservationName;
        RoomNumber = roomNumber;
        InDate = inDate;
        OutDate = outDate;
    }
}
