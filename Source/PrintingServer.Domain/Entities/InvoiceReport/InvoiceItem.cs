namespace PrintingServer.Domain.Entities;
public partial class InvoiceItem
{
    public Guid Id { get; set; }
    public Guid InvoiceID { get; set; }
    public string Details { get; set; }
    public string Cost { get; set; }
    public string TotalCost { get; set; }
    public string Currency { get; set; }
    public List<InvoiceItemTax>? invoiceItemTaxes { get; set; }
    public InvoiceItem(Guid invoiceID, string details, string cost, string totalCost, string currency)
    {
        Id = Guid.Empty;
        InvoiceID = invoiceID;
        Details = details;
        Cost = cost;
        TotalCost = totalCost;
        Currency = currency;
    }
    public InvoiceItem(Guid id, Guid invoiceID, string details, string cost, string totalCost, string currency)
    {
        Id = id;
        InvoiceID = invoiceID;
        Details = details;
        Cost = cost;
        TotalCost = totalCost;
        Currency = currency;
    }
    public void AddItem(InvoiceItemTax tax)
    {
        if (this.invoiceItemTaxes == null)
        {
            this.invoiceItemTaxes = new List<InvoiceItemTax>();
        }
        tax.Id = Guid.NewGuid();
        tax.InvoiceItemID = this.Id;
        this.invoiceItemTaxes.Add(tax);
    }
    public void AddItem(string taxname, string taxvalue)
    {
        if (this.invoiceItemTaxes == null)
        {
            this.invoiceItemTaxes = new List<InvoiceItemTax>();
        }
        InvoiceItemTax tax = new InvoiceItemTax(this.Id, taxname, taxvalue);
        tax.Id = Guid.NewGuid();
        this.invoiceItemTaxes.Add(tax);
    }
    public void AddItems(List<InvoiceItemTax> taxs)
    {
        if (this.invoiceItemTaxes == null)
        {
            this.invoiceItemTaxes = new List<InvoiceItemTax>();
        }
        foreach (InvoiceItemTax tax in taxs)
        {
            tax.Id = Guid.NewGuid();
            tax.InvoiceItemID = this.Id;
            this.invoiceItemTaxes.Add(tax);
        }
    }
}
