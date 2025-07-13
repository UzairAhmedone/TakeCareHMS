namespace TakeCareHms.Appointment;

public class Appointment
{
    public Guid AppointmentId { get; set; }

    public required DateTime AppointmentTime { get; set; }

    public required string DoctorId { get; set; }

    public string PatientId { get; set; } = string.Empty;

    public string? NurseId { get; set; }

    public List<RequiredTestReport>? TestReports { get; set; } = null;

    public DateTime BookedAt { get; set; }

    public AppointmentStatus Status { get; set; }
}

