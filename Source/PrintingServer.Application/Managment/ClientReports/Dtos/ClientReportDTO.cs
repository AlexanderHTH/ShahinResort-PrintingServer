namespace PrintingServer.Application.Managment.ClientReports.Dtos;

public class ClientReportDTO
{
    public Guid ReportId { get; set; }
    public Guid ClientId { get; set; }
    public Guid PrinterId { get; set; }
    public string ReportName { get; set; } = default!;
    public string ClientName { get; set; } = default!;
    public string ClientIp { get; set; } = default!;
    public string PrinterName { get; set; } = default!;
    public string PrinterIp { get; set; } = default!;
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
