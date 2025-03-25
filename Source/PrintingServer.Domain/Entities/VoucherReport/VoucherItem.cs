namespace PrintingServer.Domain.Entities;
public partial class VoucherItem
{
    public Guid Id { get; set; }
    public Guid VoucherID { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public VoucherItem(Guid id, Guid voucherID, string userName, string password)
    {
        Id = id;
        VoucherID = voucherID;
        UserName = userName;
        Password = password;
    }
}
