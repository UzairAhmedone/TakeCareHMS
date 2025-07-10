using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TakeCareHMS.User;

namespace TakeCareHMS.Identitiy.Services;

public class UserService : IUserService
{
    private readonly UserManager<HmsUser> userManager;
    private readonly IRepository<HmsUser> userRepository;
    private readonly SignInManager<HmsUser> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly ITokenService tokenService;

    public UserService(UserManager<HmsUser> userManager, IRepository<HmsUser> userRepository, SignInManager<HmsUser> signInManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor, ITokenService tokenService)
    {
        this.userManager = userManager;
        this.userRepository = userRepository;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
        this.httpContextAccessor = httpContextAccessor;
        this.tokenService = tokenService;
    }

    public async Task<OperationResults> RegisterDoctor(DoctorSignUpRequest request)
    {
        var results = new OperationResults();
        try
        {
            var userid = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userid != null)
            {
                var user = await userRepository.GetAsync(x => x.Id == userid);
                user.Doctor = new DoctorProfile
                {
                    LicenseNo = request.LicenseNo,
                    Specialization = request.Specialization,
                    UserId = userid
                };
                await userRepository.UpdateAsync(user);
            }
        }
        catch (Exception ex)
        {
            results.AddExceptionError(ex);
        }
        return results;
    }
    public async Task<OperationResults> RegisterNurse(NurseSignUpRequest request)
    {
        var results = new OperationResults();
        try
        {
            var userid = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userid != null)
            {
                var user = await userRepository.GetAsync(x => x.Id == userid);
                user.Nurse = new NurseProfile
                {
                    LicenseNo = request.LicenseNo,
                    Specialization = request.Specialization,
                    UserId = userid
                };
                await userRepository.UpdateAsync(user);
            }
        }
        catch (Exception ex)
        {
            results.AddExceptionError(ex);
        }
        return results;
    }
    public async Task<OperationResults> RegisterPharmacist(PharmacistSignUpRequest request)
    {
        var results = new OperationResults();
        try
        {
            var userid = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userid != null)
            {
                var user = await userRepository.GetAsync(x => x.Id == userid);
                user.Pharmacist = new PharmacistProfile
                {
                    LicenseNo = request.LicenseNo,
                    UserId = userid
                };
                await userRepository.UpdateAsync(user);
            }
        }
        catch (Exception ex)
        {
            results.AddExceptionError(ex);
        }
        return results;
    }
    public async Task<OperationResults> RegisterLabTechnician(LabTechnicianSignUpRequest request)
    {
        var results = new OperationResults();
        try
        {
            var userid = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (userid != null)
            {
                var user = await userRepository.GetAsync(x => x.Id == userid);
                user.LabTechnician = new LabTechnicianProfile
                {
                    Certification = request.Certification,
                    UserId = userid,
                };
                await userRepository.UpdateAsync(user);
            }
        }
        catch (Exception ex)
        {
            results.AddExceptionError(ex);
        }
        return results;
    }
    public async Task<OperationResults> Signup(SignupRequest request)
    {
        var results = new OperationResults();
        try
        {
            HmsUser user = request;
            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, request.UserName),
                new Claim(ClaimTypes.Email, request.Email),
                new Claim(ClaimTypes.Role, request.Role.GetEnumDescription())
            };
            var identity = new ClaimsIdentity(userClaims, "HmsAuthCookie");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            if (roleManager != null && !await roleManager.RoleExistsAsync(request.Role.GetEnumDescription()))
            {
                await roleManager.CreateAsync(new IdentityRole(request.Role.GetEnumDescription()));
            }
            await userManager.CreateAsync(user, request.Password);
            await userManager.AddToRoleAsync(user, request.Role.GetEnumDescription());
            await signInManager.SignInAsync(user, new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddHours(1)
            });

            var token = tokenService.GenerateToken(user, userClaims);
            results.Token = token;
        }
        catch (Exception ex)
        {
            results.AddExceptionError(ex);
        }
        return results;
    }
    public async Task<OperationResults> Signin(SigninRequest request)
    {
        var results = new OperationResults();
        try
        {
            var user = await userRepository.GetAsync(x => x.Email == request.Email);
            if (user is not null)
            {
                var signInResponse = await signInManager.PasswordSignInAsync(user, request.Password, true, false);
                if (signInResponse.Succeeded)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if (!roles.Contains(request.Role))
                    {
                        throw new Exception("Invalid user role.");
                    }
                    var userClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, request.Email),
                    new Claim(ClaimTypes.Role, request.Role),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };


                    var identity = new ClaimsIdentity(userClaims, "HmsAuthCookie");
                    var claimsPrincipal = new ClaimsPrincipal(identity);

                    var token = tokenService.GenerateToken(user, userClaims);
                    results.Token = token;
                }

                //await httpContextAccessor.HttpContext.SignInAsync(
                //CookieAuthenticationDefaults.AuthenticationScheme,
                //claimsPrincipal,
                //new AuthenticationProperties
                //{
                //    IsPersistent = true,
                //    ExpiresUtc = DateTime.UtcNow.AddHours(1)
                //});
            }
        }
        catch (Exception ex)
        {
            results.AddExceptionError(ex);
        }
        return results;
    }

}
