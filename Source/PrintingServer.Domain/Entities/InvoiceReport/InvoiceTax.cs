namespace PrintingServer.Domain.Entities;
public partial class InvoiceTax
{
    public Guid Id { get; set; }
    public Guid InvoiceID { get; set; }
    public string TaxName { get; set; }
    public string TaxValue { get; set; }
    public InvoiceTax(Guid id, Guid invoiceID, string taxName, string taxValue)
    {
        Id = id;
        InvoiceID = invoiceID;
        TaxName = taxName;
        TaxValue = taxValue;
    }
}
