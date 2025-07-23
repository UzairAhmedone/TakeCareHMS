
namespace TakeCareHms.Appointment;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository appointmentRepository;

    public AppointmentService(IAppointmentRepository appointmentRepository)
    {
        this.appointmentRepository = appointmentRepository;
    }

    public Task BookAsync(AppointmentRequest request, CancellationToken cancellationToken = default)
    {
        // Validate the request
        Appointment appointment = null;
        appointmentRepository.BookAsync(appointment, cancellationToken);
        throw new NotImplementedException();
    }
}
