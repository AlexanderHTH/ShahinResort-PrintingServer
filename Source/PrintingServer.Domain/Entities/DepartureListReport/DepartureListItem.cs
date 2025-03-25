namespace PrintingServer.Domain.Entities;
public class DepartureListItem
{
    public Guid Id { get; set; }
    public Guid ListID { get; set; }
    public string ReservationNumber { get; set; }
    public string FullName { get; set; }
    public string RoomNumber { get; set; }
    public string NightCount { get; set; }
    public string InDate { get; set; }
    public string OutDate { get; set; }
    public string Company { get; set; }
    public string CompanyName { get; set; }
    public string Group { get; set; }
    public string RoomState { get; set; }
    public DepartureListItem(Guid id, Guid listID, string reservationNumber, string fullName, string roomNumber, string nightCount, string inDate, string outDate, string company, string companyName, string group, string roomState)
    {
        Id = id;
        ListID = listID;
        ReservationNumber = reservationNumber;
        FullName = fullName;
        RoomNumber = roomNumber;
        NightCount = nightCount;
        InDate = inDate;
        OutDate = outDate;
        Company = company;
        CompanyName = companyName;
        Group = group;
        RoomState = roomState;
    }
}
