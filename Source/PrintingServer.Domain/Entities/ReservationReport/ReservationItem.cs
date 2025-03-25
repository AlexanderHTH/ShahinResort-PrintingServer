namespace PrintingServer.Domain.Entities;
public partial class ReservationItem
{
    public Guid Id { get; set; }
    public Guid ReservationID { get; set; }
    public string CustomerName { get; set; }
    public string SuiteNu { get; set; }
    public string FromDate { get; set; }
    public string ToDate { get; set; }
    public string StayDuration { get; set; }
    public string SingleNightWithTax { get; set; }
    public string TotalWithTax { get; set; }
    public ReservationItem(Guid id, Guid reservationID, string customerName, string suiteNu, string fromDate, string toDate, string stayDuration, string singleNightWithTax, string totalWithTax)
    {
        Id = id;
        ReservationID = reservationID;
        CustomerName = customerName;
        SuiteNu = suiteNu;
        FromDate = fromDate;
        ToDate = toDate;
        StayDuration = stayDuration;
        SingleNightWithTax = singleNightWithTax;
        TotalWithTax = totalWithTax;
    }
}
