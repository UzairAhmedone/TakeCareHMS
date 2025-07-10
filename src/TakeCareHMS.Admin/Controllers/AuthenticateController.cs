using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TakeCareHMS.Identitiy;
using TakeCareHMS.Identitiy.Services;

namespace TakeCareHMS.Admin.Controllers;

[Route("api/[controller]")]
[AllowAnonymous]
[ApiController]
public class AuthenticateController : Controller
{
    private readonly IUserService userService;

    public AuthenticateController(IUserService userService, IConfiguration configuration)
    {   
        this.userService = userService;
    }
    [HttpPost("Signup/Doctor")]
    public async Task<IActionResult> SignupDoctor([FromBody] DoctorSignUpRequest request)
    {
        var registerDoctorResponse = await userService.RegisterDoctor(request);
        return Ok(registerDoctorResponse);
    }
    [HttpPost("Signup/Nurse")]
    public async Task<IActionResult> SignupNurse([FromBody] NurseSignUpRequest request)
    {
        var registerDoctorResponse = await userService.RegisterNurse(request);
        return Ok(registerDoctorResponse);
    }
    [HttpPost("Signup/Pharmacist")]
    public async Task<IActionResult> SignupPharmacist([FromBody] PharmacistSignUpRequest request)
    {
        var registerDoctorResponse = await userService.RegisterPharmacist(request);
        return Ok(registerDoctorResponse);
    }
    [HttpPost("Signup/LabTechnician")]
    public async Task<IActionResult> SignupLabTechnician([FromBody] LabTechnicianSignUpRequest request)
    {
        var registerDoctorResponse = await userService.RegisterLabTechnician(request);
        return Ok(registerDoctorResponse);
    }
    [HttpPost("Signup")]
    public async Task<IActionResult> Signup([FromBody] SignupRequest request)
    {
        var registerDoctorResponse = await userService.Signup(request);
        Response.Cookies.Append("HMS_Auth_Token", registerDoctorResponse.Token);
        return Ok(registerDoctorResponse);
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] SigninRequest request)
    {
        var registerDoctorResponse = await userService.Signin(request);
        Response.Cookies.Append("HMS_Auth_Token", registerDoctorResponse.Token);
        return Ok(registerDoctorResponse);
    }
}
