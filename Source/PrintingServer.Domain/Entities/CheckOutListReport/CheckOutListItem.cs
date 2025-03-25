namespace PrintingServer.Domain.Entities;

public partial class CheckOutListItem
{
    public Guid Id { get; set; }
    public Guid ListID { get; set; }
    public string RefferanceNumber { get; set; }
    public string ReservationName { get; set; }
    public string RoomNumber { get; set; }
    public string InDate { get; set; }
    public string OutDate { get; set; }
    public string ExtraField { get; set; }
    public CheckOutListItem(Guid id, Guid listID, string refferanceNumber, string reservationName, string roomNumber, string inDate, string outDate, string extraField)
    {
        Id = id;
        ListID = listID;
        RefferanceNumber = refferanceNumber;
        ReservationName = reservationName;
        RoomNumber = roomNumber;
        InDate = inDate;
        OutDate = outDate;
        ExtraField = extraField;
    }
}
