namespace PrintingServer.Domain.Entities;
public partial class Voucher
{
    public Guid Id { get; set; }
    public string SuiteNumber { get; set; }
    public string CreatedBy { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public List<VoucherItem>? Vouchers { get; set; }
    public Voucher(Guid id, string suiteNumber, string createdBy, string from, string to)
    {
        Id = id;
        SuiteNumber = suiteNumber;
        CreatedBy = createdBy;
        From = from;
        To = to;
    }
}
