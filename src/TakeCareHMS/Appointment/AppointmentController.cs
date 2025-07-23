using Microsoft.AspNetCore.Mvc;

namespace TakeCareHms.Appointment;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        this.appointmentService = appointmentService;
    }
    
    [HttpPost("book")]
    public IActionResult Book(AppointmentRequest request)
    {
        appointmentService.BookAsync(request);
        return Ok();
    }

    [HttpPost("cancel")]
    public IActionResult Cancel()
    {
        return Ok();
    }

    [HttpPost("postpone")]
    public IActionResult Postpone()
    {
        return Ok();
    }
}
