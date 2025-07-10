using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TakeCareHMS.User;

public class HmsUser : IdentityUser
{
    [Required]
    [MaxLength(64)]
    public string FullName { get; set; } = string.Empty;

    private int age;
    [Required]
    public int Age
    {
        get { return age; }
        set
        {
            if (value > 10 && value < 150)
            {
                age = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException();
            }
        }
    }
    [MaxLength(3)]
    [MinLength(2)]
    public string Nationality { get; set; } = string.Empty;
    [MaxLength(32)]
    public string City { get; set; } = string.Empty;
    [MaxLength(32)]
    public string Street { get; set; } = string.Empty;
    [MaxLength(8)]
    public string PlotNo { get; set; } = string.Empty;
    [MaxLength(8)]
    public string? FlatNo { get; set; }
    [MaxLength(8)]
    public string PostalCode { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public virtual DoctorProfile? Doctor { get; set; }
    public virtual NurseProfile? Nurse { get; set; }
    public virtual PharmacistProfile? Pharmacist { get; set; }
    public virtual LabTechnicianProfile? LabTechnician { get; set; }
}

