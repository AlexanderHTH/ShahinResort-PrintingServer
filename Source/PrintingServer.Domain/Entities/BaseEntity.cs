using System.Text.Json;

namespace PrintingServer.Domain.Entities;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AppUserId { get; set; }
    public string Name { get; set; } = "Auto Generated.";
    public string? Description { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.Now;
    public DateTime? UpdatedOn { get; set; } = null;
    public bool IsDeleted { get; set; } = false;
    public bool IsModified { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public void AddNote(string note) => Notes = Notes + Environment.NewLine + note + (UpdatedOn.HasValue ? " on" + UpdatedOn.Value.ToString("dd/MM/yyyy hh:mm:ss tt") : "");
    public void Activate()
    {
        IsActive = true;
        Modify();
        AddNote("Activated");
    }
    public void Deactivate()
    {
        IsActive = false;
        Modify();
        AddNote("Deactivated");
    }
    public void Modify()
    {
        IsModified = true;
        UpdatedOn = DateTime.Now;
        AddNote("Modified");
    }
    public void Delete()
    {
        IsDeleted = true;
        IsActive = false;
        Modify();
        AddNote("Deleted");
    }
    public void UnDelete()
    {
        IsDeleted = false;
        Modify();
        AddNote("Undeleted");
    }
    public void Update(bool isdeleted, bool isactive, string name = "", string description = "")
    {
        IsDeleted = isdeleted;
        IsActive = isactive;
        Name = name;
        Description = description;
        IsModified = true;
        UpdatedOn = DateTime.Now;
        AddNote("Updated");
    }
}
