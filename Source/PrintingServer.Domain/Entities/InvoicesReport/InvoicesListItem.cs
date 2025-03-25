namespace PrintingServer.Domain.Entities;
public partial class InvoicesListItem
{
    public Guid Id { get; set; }
    public Guid ListID { get; set; }
    public string ItemDate { get; set; }
    public string InvoicesNumbers { get; set; }
    public string InvoicesValue { get; set; }
    public string Tax1Total { get; set; }
    public string Tax2Total { get; set; }
    public string Tax3Total { get; set; }
    public string Total { get; set; }
    public InvoicesListItem(Guid id, Guid listID, string itemDate, string invoicesNumbers, string invoicesValue, string tax1Total, string tax2Total, string tax3Total, string total)
    {
        Id = id;
        ListID = listID;
        ItemDate = itemDate;
        InvoicesNumbers = invoicesNumbers;
        InvoicesValue = invoicesValue;
        Tax1Total = tax1Total;
        Tax2Total = tax2Total;
        Tax3Total = tax3Total;
        Total = total;
    }
}
