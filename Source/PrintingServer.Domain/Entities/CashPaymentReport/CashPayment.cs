using System.Reflection.Metadata.Ecma335;

namespace PrintingServer.Domain.Entities;
public partial class CashPayment
{
    public Guid Id { get; set; }
    public string CreatedBy { get; set; }
    public string DepitAccountName { get; set; }
    public string CreditAccountName { get; set; }
    public string DepitValue { get; set; }
    public string CreditValue { get; set; }
    public string TotalValue { get; set; }
    public string Currency { get; set; }
    public string AccountName { get; set; }
    public CashPayment(Guid id, string createdBy, string depitAccountName, string creditAccountName, string depitValue, string creditValue, string totalValue, string currency, string accountName)
    {
        Id = id;
        CreatedBy = createdBy;
        DepitAccountName = depitAccountName;
        CreditAccountName = creditAccountName;
        DepitValue = depitValue;
        CreditValue = creditValue;
        TotalValue = totalValue;
        Currency = currency;
        AccountName = accountName;
    }

}
