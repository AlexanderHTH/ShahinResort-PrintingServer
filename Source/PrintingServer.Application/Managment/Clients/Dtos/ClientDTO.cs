using PrintingServer.Application.Managment.ClientReports.Dtos;

namespace PrintingServer.Application.Managment.Clients.Dtos;

public class ClientDTO
{
    public string ClientName { get; set; } = default!;
    public string ClientIp { get; set; } = default!;
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
    public List<ClientReportDTO> ClientReports { get; set; } = [];
}
