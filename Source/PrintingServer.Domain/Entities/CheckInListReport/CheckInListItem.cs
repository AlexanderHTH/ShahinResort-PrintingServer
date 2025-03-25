namespace PrintingServer.Domain.Entities;

public partial class CheckInListItem
{
    public Guid Id { get; set; }
    public Guid ListID { get; set; }
    public string RefferanceNumber { get; set; }
    public string ReservationName { get; set; }
    public string RoomNumber { get; set; }
    public string InDate { get; set; }
    public string OutDate { get; set; }
    public CheckInListItem(Guid id, Guid listID, string refferanceNumber, string reservationName, string roomNumber, string inDate, string outDate)
    {
        Id = id;
        ListID = listID;
        RefferanceNumber = refferanceNumber;
        ReservationName = reservationName;
        RoomNumber = roomNumber;
        InDate = inDate;
        OutDate = outDate;
    }
}
