namespace PrintingServer.Domain.Entities;
public partial class Reservation
{
    public Guid Id { get; set; }
    public string ReservationNumber { get; set; }
    public string CreationDate { get; set; }
    public string Total { get; set; }
    public string TotalForg { get; set; }
    public string CreatedBy { get; set; }
    public string PrintedBy { get; set; }
    public List<ReservationItem>? Items { get; set; }
    public Reservation(Guid id, string reservationNumber, string creationDate, string total, string totalForg, string createdBy, string printedBy)
    {
        Id = id;
        ReservationNumber = reservationNumber;
        CreationDate = creationDate;
        Total = total;
        TotalForg = totalForg;
        CreatedBy = createdBy;
        PrintedBy = printedBy;
    }
}
