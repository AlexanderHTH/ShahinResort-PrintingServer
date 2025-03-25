namespace PrintingServer.Application.Managment.Countries.Dtos;

public class CountryDTO
{
    public string CountryCode { get; set; } = default!;
    public string CountryEnName { get; set; } = default!;
    public string CountryArName { get; set; } = default!;
    public string CountryEnNationality { get; set; } = default!;
    public string CountryArNationality { get; set; } = default!;
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
