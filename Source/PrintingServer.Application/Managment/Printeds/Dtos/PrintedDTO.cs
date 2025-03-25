namespace PrintingServer.Application.Managment.Printeds.Dtos;

public class PrintedDTO
{
    public Guid ClientReportId { get; set; }
    public DateTime PrintedTime { get; set; }
    public string MainObjectGuid { get; set; } = default!;
    public string PrintedBy { get; set; } = default!;
    public Guid Id { get; set; }
    public Guid AppUserId { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsModified { get; set; }
    public bool IsActive { get; set; }
}
