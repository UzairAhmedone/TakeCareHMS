using System.Security.Claims;
using TakeCareHMS.Identitiy;
using TakeCareHMS.User;

public interface ITokenService
{
    string GenerateToken(HmsUser user, List<Claim> claims);
}