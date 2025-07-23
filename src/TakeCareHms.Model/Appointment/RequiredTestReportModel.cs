using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeCareHms.Appointment;

public class RequiredTestReportModel
{
    public Guid Id { get; set; }

    public DateTime UploadDate { get; set; }

    public required string ReportContent { get; set; }

    // Back‑reference to the aggregate root
    public Guid AppointmentId { get; set; }
}
