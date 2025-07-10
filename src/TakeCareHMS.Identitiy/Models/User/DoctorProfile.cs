using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TakeCareHMS.User;

public class DoctorProfile
{
    [Key]
    public Guid Id { get; set; }
    [MaxLength(32)]
    public string Specialization { get; set; } = string.Empty;
    [MaxLength(32)]
    public string LicenseNo { get; set; } = string.Empty;
    [ForeignKey("UserId")]
    public string UserId { get; set; }
    public virtual HmsUser User { get; set; }
}
