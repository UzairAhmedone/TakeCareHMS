namespace TakeCareHms.Appointment;

public class AppointmentRequest
{
    public int Id { get; set; }
    public DateTime AppointmentDate { get; set; }
    public required string DoctorId { get; set; }
    public required string PatientId { get; set; }
    public required string Status { get; set; }
    public string Notes { get; set; } // Additional notes or comments
}
