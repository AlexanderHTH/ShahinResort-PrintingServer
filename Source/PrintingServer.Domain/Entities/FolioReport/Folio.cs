namespace PrintingServer.Domain.Entities;
public partial class Folio
{
    public Guid Id { get; set; }
    public string CreatedBy { get; set; }
    public string SuiteNumber { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public string Payment { get; set; }
    public string Rquired { get; set; }
    public string TotalAllRegistration { get; set; }
    public string TotalConfirmedPayment { get; set; }
    public string TotalUnconfirmedPayment { get; set; }
    public string TotalConfirmedINS { get; set; }
    public string TotalUnconfirmedINS { get; set; }
    public string TotalTransfare { get; set; }
    public string TotalAllRequired { get; set; }
    public List<AllRegistration>? AllRegistration { get; set; }
    public List<ConfirmedPayment>? ConfirmedPayment { get; set; }
    public List<UnConfirmedPayment>? UnconfirmedPayment { get; set; }
    public List<ConfirmedINS>? ConfirmedINS { get; set; }
    public List<UnConfirmedINS>? UnconfirmedINS { get; set; }
    public List<Transfare>? Transfare { get; set; }
    public List<RequiredItem>? AllRequired { get; set; }
    public Folio(Guid id, string createdBy, string suiteNumber, string from, string to, string payment, string rquired, string totalAllRegistration, string totalConfirmedPayment, string totalUnconfirmedPayment, string totalConfirmedINS, string totalUnconfirmedINS, string totalTransfare, string totalAllRequired)
    {
        Id = id;
        CreatedBy = createdBy;
        SuiteNumber = suiteNumber;
        From = from;
        To = to;
        Payment = payment;
        Rquired = rquired;
        TotalAllRegistration = totalAllRegistration;
        TotalConfirmedPayment = totalConfirmedPayment;
        TotalUnconfirmedPayment = totalUnconfirmedPayment;
        TotalConfirmedINS = totalConfirmedINS;
        TotalUnconfirmedINS = totalUnconfirmedINS;
        TotalTransfare = totalTransfare;
        TotalAllRequired = totalAllRequired;
    }

}
