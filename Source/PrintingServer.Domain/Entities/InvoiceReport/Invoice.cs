namespace PrintingServer.Domain.Entities;
public partial class Invoice
{
    public Guid Id { get; set; }
    public int IncrementalNumber { get; set; }
    public int IncrementalCheckInNumber { get; set; }
    public string InvoceName { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsSch { get; set; }
    public bool IsPrintedOnce { get; set; }
    public string CurrencyName { get; set; }
    public string Code { get; set; }
    public string RandomNumber { get; set; }
    public string CreatedBy { get; set; }
    public string CreatedOn { get; set; }
    public string NightCount { get; set; }
    public string CostWithoutTax { get; set; }
    public string TotalTax { get; set; }
    public string TotalCost { get; set; }
    public byte[] QRCode { get; set; }
    public string Discount { get; set; }
    public string TotalAfterGivenDiscount { get; set; }
    public string CheckIn { get; set; }
    public string CheckOut { get; set; }
    public List<InvoiceItem> Items { get; set; }
    public List<InvoiceTax> Taxs { get; set; }
    public Invoice(Guid id, int incrementalNumber, int incrementalCheckInNumber, string invoceName, string phoneNumber, bool isSch, bool isPrintedOnce, string currencyName, string code, string randomNumber, string createdBy, string createdOn, string nightCount, string costWithoutTax, string totalTax, string totalCost, byte[] qRCode, string discount, string totalAfterGivenDiscount, string checkIn, string checkOut)
    {
        Id = id;
        IncrementalNumber = incrementalNumber;
        IncrementalCheckInNumber = incrementalCheckInNumber;
        InvoceName = invoceName;
        PhoneNumber = phoneNumber;
        IsSch = isSch;
        IsPrintedOnce = isPrintedOnce;
        CurrencyName = currencyName;
        Code = code;
        RandomNumber = randomNumber;
        CreatedBy = createdBy;
        CreatedOn = createdOn;
        NightCount = nightCount;
        CostWithoutTax = costWithoutTax;
        TotalTax = totalTax;
        TotalCost = totalCost;
        QRCode = qRCode;
        Discount = discount;
        TotalAfterGivenDiscount = totalAfterGivenDiscount;
        CheckIn = checkIn;
        CheckOut = checkOut;
        Items = new List<InvoiceItem>();
        Taxs = new List<InvoiceTax>();
    }
    public void AddItem(InvoiceItem item)
    {
        if (this.Items == null)
        {
            this.Items = new List<InvoiceItem>();
        }
        item.Id = Guid.NewGuid();
        item.InvoiceID = this.Id;
        this.Items.Add(item);
    }
    public void AddItem(string details, string cost, string totalcost, string currency)
    {
        if (this.Items == null)
        {
            this.Items = new List<InvoiceItem>();
        }
        InvoiceItem item = new InvoiceItem(this.Id, details, cost, totalcost, currency);
        item.Id = Guid.NewGuid();
        this.Items.Add(item);
    }
    public void AddItems(List<InvoiceItem> items)
    {
        if (this.Items == null)
        {
            this.Items = new List<InvoiceItem>();
        }
        foreach (InvoiceItem item in items)
        {
            item.Id = Guid.NewGuid();
            item.InvoiceID = this.Id;
            this.Items.Add(item);
        }
    }

    public void AddTaxs(List<InvoiceTax> items)
    {
        if (this.Taxs == null)
        {
            this.Taxs = new List<InvoiceTax>();
        }
        if (this.Items != null)
        {
            foreach (InvoiceTax item in items)
            {
                item.Id = Guid.NewGuid();
                item.InvoiceID = this.Id;
                this.Taxs.Add(item);
            }
        }
    }

    public List<InvoiceItemTax> AllTaxs()
    {
        List<InvoiceItemTax> toreturn = new List<InvoiceItemTax>();
        if (this.Items != null)
        {
            foreach (InvoiceItem item in this.Items)
            {
                if (item.invoiceItemTaxes != null)
                {
                    toreturn.AddRange(item.invoiceItemTaxes);
                }
            }
        }
        return toreturn;
    }
}
