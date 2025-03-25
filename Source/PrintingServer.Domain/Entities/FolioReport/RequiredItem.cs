namespace PrintingServer.Domain.Entities;
public partial class RequiredItem
{
    public Guid Id { get; set; }
    public Guid FolioID { get; set; }
    public string NDate { get; set; }
    public string Offer { get; set; }
    public string RateBeforeTax { get; set; }
    public string RateAfterTax { get; set; }
    public RequiredItem(Guid id, Guid folioID, string nDate, string offer, string rateBeforeTax, string rateAfterTax)
    {
        Id = id;
        FolioID = folioID;
        NDate = nDate;
        Offer = offer;
        RateBeforeTax = rateBeforeTax;
        RateAfterTax = rateAfterTax;
    }
}
