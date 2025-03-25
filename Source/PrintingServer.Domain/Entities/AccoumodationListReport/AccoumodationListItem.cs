namespace PrintingServer.Domain.Entities;
public partial class AccoumodationListItem
{
    public Guid Id { get; set; }
    public Guid ListID {  get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FatherName { get; set; }
    public string MotherName { get; set; }
    public string Nationality { get; set; }
    public string POB { get; set; }
    public string DOB { get; set; }
    public string Job { get; set; }
    public string LivingIn { get; set; }
    public string CommingFrom { get; set; }
    public string CommingDate { get; set; }
    public string LeavingFrom { get; set; }
    public string LeavingDate { get; set; }
    public string PD_Source { get; set; }
    public string PD_Number { get; set; }
    public string PD_Date { get; set; }
    public string RoomNumber { get; set; }
    public AccoumodationListItem(Guid id, Guid listID, string firstName, string lastName, string fatherName, string motherName, string nationality, string pOB, string dOB, string job, string livingIn, string commingFrom, string commingDate, string leavingFrom, string leavingDate, string pD_Source, string pD_Number, string pD_Date, string roomNumber)
    {
        Id = id;
        ListID = listID;
        FirstName = firstName;
        LastName = lastName;
        FatherName = fatherName;
        MotherName = motherName;
        Nationality = nationality;
        POB = pOB;
        DOB = dOB;
        Job = job;
        LivingIn = livingIn;
        CommingFrom = commingFrom;
        CommingDate = commingDate;
        LeavingFrom = leavingFrom;
        LeavingDate = leavingDate;
        PD_Source = pD_Source;
        PD_Number = pD_Number;
        PD_Date = pD_Date;
        RoomNumber = roomNumber;
    }

}
