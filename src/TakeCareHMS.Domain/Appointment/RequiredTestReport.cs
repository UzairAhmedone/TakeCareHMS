namespace TakeCareHms.Appointment;

public class RequiredTestReport
{
    public Guid Id { get; set; }

    public DateTime UploadDate { get; set; }

    public required string ReportContent { get; set; }

    // Back‑reference to the aggregate root
    public Guid AppointmentId { get; set; }

    public required Appointment Appointment { get; set; }
}
