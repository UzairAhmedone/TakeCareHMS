using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TakeCareHMS.Identitiy;
using TakeCareHMS.Identitiy.Services;

namespace TakeCareHMS.Admin.Controllers;

[Route("api/[controller]")]
[AllowAnonymous]
[ApiController]
public class AuthController : Controller
{
    private readonly IUserService userService;

    public AuthController(IUserService userService, IConfiguration configuration)
    {   
        this.userService = userService;
    }
    
    [HttpPost("Signup")]
    public async Task<IActionResult> Signup([FromBody] SignupRequest request)
    {   
        var registerDoctorResponse = await userService.Signup(request);
        Response.Headers.Append("HMS_Auth_Token", registerDoctorResponse.Token);
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
