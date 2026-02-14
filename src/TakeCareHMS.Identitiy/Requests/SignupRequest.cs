using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using TakeCareHMS.User;
namespace TakeCareHMS.Identitiy;

public class SignupRequest
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;

    public string FullName { get{ return $"{FirstName} {LastName}"; }}
    [Required]
    [PasswordPropertyText]
    public string Password { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string UserName { get; set; } = string.Empty;

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
    public string City { get; set; } = string.Empty;
    public string Street { get; set; } = string.Empty;
    public string PlotNo { get; set; } = string.Empty;
    public string? FlatNo { get; set; }
    public string PostalCode { get; set; } = string.Empty;
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public HmsRoles Role { get; set; } = HmsRoles.Patients;
    // Conditional profiles (only one will be populated)
    public DoctorSignUpRequest? DoctorProfile { get; set; }
    public NurseSignUpRequest? NurseProfile { get; set; }
    public PharmacistSignUpRequest? PharmacistProfile { get; set; }
    public LabTechnicianSignUpRequest? LabTechnicianProfile { get; set; }
    public static implicit operator HmsUser(SignupRequest request)
    {
        return new HmsUser
        {
            Age = request.Age,
            Street = request.Street,
            PlotNo = request.PlotNo,
            FlatNo = request.FlatNo,
            PostalCode = request.PostalCode,
            Gender = request.Gender,
            City = request.City,
            Email = request.Email,
            FullName = request.FullName,
            UserName = request.UserName,
            PhoneNumber = request.PhoneNumber,
            Nationality = request.Nationality,

            Doctor = request.DoctorProfile == null ? null : new DoctorProfile
            {
                Specialization = request.DoctorProfile.Specialization,
                LicenseNo = request.DoctorProfile.LicenseNo
            },

            Nurse = request.NurseProfile == null ? null : new NurseProfile
            {
                Specialization = request.NurseProfile.Specialization,
                LicenseNo = request.NurseProfile.LicenseNo
            },

            Pharmacist = request.PharmacistProfile == null ? null : new PharmacistProfile
            {
                LicenseNo = request.PharmacistProfile.LicenseNo
            },

            LabTechnician = request.LabTechnicianProfile == null ? null : new LabTechnicianProfile
            {
                Certification = request.LabTechnicianProfile.Certification
            }
        };
    }
}
public class DoctorSignUpRequest
{
    public string Specialization { get; set; } = string.Empty;
    public string LicenseNo { get; set; } = string.Empty;
}

public class NurseSignUpRequest
{
    public string Specialization { get; set; } = string.Empty;
    public string LicenseNo { get; set; } = string.Empty;
}
public class PharmacistSignUpRequest
{
    public string LicenseNo { get; set; } = string.Empty;
}
public class LabTechnicianSignUpRequest
{
    public string Certification { get; set; } = string.Empty;
}
