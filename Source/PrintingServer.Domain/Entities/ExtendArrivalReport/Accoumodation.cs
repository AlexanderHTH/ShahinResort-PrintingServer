namespace PrintingServer.Domain.Entities;
public partial class Accoumodation
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Mobile { get; set; }
    public string NumberOfAdult { get; set; }
    public string NumberOfChild { get; set; }
    public string NumberOfYoung { get; set; }
    public string RefranceNumber { get; set; }
    public List<AccoumodationItem>? Items { get; set; }
    public Accoumodation(Guid id, string name, string mobile, string numberOfAdult, string numberOfChild, string numberOfYoung, string refranceNumber)
    {
        Id = id;
        Name = name;
        Mobile = mobile;
        NumberOfAdult = numberOfAdult;
        NumberOfChild = numberOfChild;
        NumberOfYoung = numberOfYoung;
        RefranceNumber = refranceNumber;
    }
}
