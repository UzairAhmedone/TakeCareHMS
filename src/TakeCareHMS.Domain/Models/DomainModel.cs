namespace TakeCareHMS.Domain;

public abstract class DomainModel
{
    public DomainModel()
    {
        CreatedAt = DateTime.UtcNow;
    }
    public string UserId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public abstract class ModifiableDomainModel : DomainModel
{
    public ModifiableDomainModel()
    {
        ModifiedAt = DateTime.UtcNow;
    }
    public DateTime ModifiedAt { get; set; }
    public string ModifiedByUserId { get; set; } = string.Empty;

}