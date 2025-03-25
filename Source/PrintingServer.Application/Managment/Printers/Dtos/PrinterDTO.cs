using PrintingServer.Domain.Entities;

namespace PrintingServer.Application.Managment.Printers.Dtos;

public class PrinterDTO
{
    public string PrinterName { get; set; } = default!;
    public string PrinterIp { get; set; } = default!;
    public int ResponseTime { get; set; } = 150;
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
    public List<PrinterErrorLog> PrinterErrorLogs { get; set; } = [];

}
