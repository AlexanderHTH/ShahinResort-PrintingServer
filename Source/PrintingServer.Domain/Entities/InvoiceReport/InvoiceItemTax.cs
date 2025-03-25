namespace PrintingServer.Domain.Entities; public partial class InvoiceItemTax
{
    public Guid Id { get; set; }
    public Guid InvoiceItemID { get; set; }
    public string TaxName { get; set; }
    public string TaxValue { get; set; }
    public InvoiceItemTax(Guid invoiceItemID, string taxName, string taxValue)
    {
        Id = Guid.Empty;
        InvoiceItemID = invoiceItemID;
        TaxName = taxName;
        TaxValue = taxValue;
    }
    public InvoiceItemTax(Guid id, Guid invoiceItemID, string taxName, string taxValue)
    {
        Id = id;
        InvoiceItemID = invoiceItemID;
        TaxName = taxName;
        TaxValue = taxValue;
    }
}

