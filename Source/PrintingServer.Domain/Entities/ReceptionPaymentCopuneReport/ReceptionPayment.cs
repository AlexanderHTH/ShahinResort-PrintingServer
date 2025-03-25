namespace PrintingServer.Domain.Entities;
public partial class ReceptionPayment
{
    public Guid Id { get; set; }
    public string Date { get; set; }
    public string Amount { get; set; }
    public string Name { get; set; }
    public string Room { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public string CreatedBy { get; set; }
    public bool PType { get; set; }
    public string Currency { get; set; }
    public ReceptionPayment(Guid id, string date, string amount, string name, string room, string from, string to, string createdBy, bool pType, string currency)
    {
        Id = id;
        Date = date;
        Amount = amount;
        Name = name;
        Room = room;
        From = from;
        To = to;
        CreatedBy = createdBy;
        PType = pType;
        Currency = currency;
    }
}
