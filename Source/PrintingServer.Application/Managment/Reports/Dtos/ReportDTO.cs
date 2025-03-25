using PrintingServer.Domain.Entities;

namespace PrintingServer.Application.Managment.Reports.Dtos;

public class ReportDTO
{
    public Guid PrinterId { get; set; }
    public string ReportName { get; set; } = default!;
    public string ReportPath { get; set; } = default!;
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
    public List<ClientReport> ClientReports { get; set; } = [];
}
