using Microsoft.EntityFrameworkCore;
using TakeCareHMS.Identitiy.ModelConfigurations;
using TakeCareHMS.Profiles;

namespace TakeCareHMS.Identitiy.Configurations;

internal static class ModelConfigruations
{
    public static void RegisterIdentityModelConfigs(this ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new HmsUserConfiguration());
        modelBuilder.ApplyConfiguration(new DoctorProfileConfiguration());
        modelBuilder.ApplyConfiguration(new NurseProfileConfiguration());
        modelBuilder.ApplyConfiguration(new PharmacistsProfileConfiguration());
        modelBuilder.ApplyConfiguration(new LabTechnicianProfileConfiguration());
    }

    public static void SeedAdminRole(this ModelBuilder modelBuilder)
    {
        //modelBuilder.Entity<HmsUser>()
    }
}
