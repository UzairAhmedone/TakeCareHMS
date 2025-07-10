using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TakeCareHMS.Profiles;
using TakeCareHMS.User;
namespace TakeCareHMS.Identitiy;

public class SignupRequest
{
    [Required]
    public string FullName { get; set; } = string.Empty;

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
    public HmsRoles Role { get; set; }
    public static implicit operator HmsUser(SignupRequest request)
    {
        return new HmsUser
        {
            Age = request.age,
            Street = request.Street,
            PlotNo = request.PlotNo,
            FlatNo = request.FlatNo,
            PostalCode = request.PostalCode,
            Gender = request.Gender,
            City = request.City,
            Email = request.Email,
            FullName = request.FullName,
            PhoneNumber = request.PhoneNumber,
            Nationality = request.Nationality,
            UserName = request.UserName,
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
