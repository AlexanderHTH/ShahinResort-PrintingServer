using PrintingServer.Domain.Entities;

namespace PrintingServer.Application.Managment.PrinterErrorLogs.Dtos;

public class PrinterErrorLogDTO
{
    public Guid PrinterId { get; set; }

    public DateTime ErrorDate { get; set; }

    public string Details { get; set; } = default!;

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
    public Printer Printer { get; set; } = default!;
}
