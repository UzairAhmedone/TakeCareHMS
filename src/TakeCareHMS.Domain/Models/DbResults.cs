namespace TakeCareHms.Repositories;

public class DbResults
{
    public bool IsSuccess { get; set; }
    public ICollection<string> Errors { get; set; } = new List<string>();
    public void AddError(string error)
    {
        Errors.Add(error);
    }
}
