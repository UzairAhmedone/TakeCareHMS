namespace TakeCareHms.Appointment;

public interface IAppointmentService
{
    Task BookAsync(AppointmentRequest request, CancellationToken cancellationToken = default);
}
