namespace PrintingServer.Domain.Entities;

public class Transfare
{
    public Guid Id { get; set; }
    public Guid FolioID { get; set; }
    public string NDate { get; set; }
    public string Amount { get; set; }
    public string Details { get; set; }
    public string AccountName { get; set; }
    public Transfare(Guid id, Guid folioID, string nDate, string amount, string details, string accountName)
    {
        Id = id;
        FolioID = folioID;
        NDate = nDate;
        Amount = amount;
        Details = details;
        AccountName = accountName;
    }
}
