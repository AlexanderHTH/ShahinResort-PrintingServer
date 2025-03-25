namespace PrintingServer.Domain.Entities;
public partial class InvoicesList
{
    public Guid Id { get; set; }
    public string PrintedBy { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public string PrintedAt { get; set; }
    public string Currency { get; set; }
    public string TotalInvoices { get; set; }
    public string TotalTax1 { get; set; }
    public string TotalTax2 { get; set; }
    public string TotalTax3 { get; set; }
    public string TTotal { get; set; }
    public List<InvoicesListItem>? Items { get; set; }
    public InvoicesList(Guid id, string printedBy, string startDate, string endDate, string printedAt, string currency, string totalInvoices, string totalTax1, string totalTax2, string totalTax3, string tTotal)
    {
        Id = id;
        PrintedBy = printedBy;
        StartDate = startDate;
        EndDate = endDate;
        PrintedAt = printedAt;
        Currency = currency;
        TotalInvoices = totalInvoices;
        TotalTax1 = totalTax1;
        TotalTax2 = totalTax2;
        TotalTax3 = totalTax3;
        TTotal = tTotal;
    }
}
