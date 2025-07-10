namespace TakeCareHMS;

public class OperationResults
{
    public string Token { get; set; }
    public HashSet<string> UserErrors { get; private set; } = [];
    public HashSet<string> ExceptionErrors { get; private set; } = [];
    public HashSet<string> Warnings { get; private set; } = [];
    public HashSet<string> LogInfo { get; private set; } = [];

    public void AddUserError(params string[] errors)
    {
        foreach (var err in errors)
        {
            UserErrors.Add(err);
        }
    }
    public void AddExceptionError(params string[] errors)
    {
        foreach (var err in errors)
        {
            UserErrors.Add(err);
        }
    }
    public void AddExceptionError(Exception errors)
    {
        ExceptionErrors.Add(errors.Message);
    }

}
