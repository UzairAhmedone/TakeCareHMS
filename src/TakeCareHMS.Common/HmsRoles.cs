using System.ComponentModel;

namespace TakeCareHMS;

public enum HmsRoles
{
    [Description("Admin")]
    Admin,
    [Description("Doctor")]
    Doctor,
    [Description("Patient")]
    Patients,
    [Description("Receptionist")]
    Receptionists,
    [Description("Nurse")]
    Nurses,
    [Description("Pharmacist")]
    Pharmacists,
    [Description("LabTechnician")]
    LabTechnicians
}
