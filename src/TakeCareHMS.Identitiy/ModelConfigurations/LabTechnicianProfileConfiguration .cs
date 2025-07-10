using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeCareHMS.User;

namespace TakeCareHMS.Profiles
{
    internal class LabTechnicianProfileConfiguration : IEntityTypeConfiguration<LabTechnicianProfile>
    {
        public void Configure(EntityTypeBuilder<LabTechnicianProfile> builder)
        {
            // Set the primary key
            builder.HasKey(dp => dp.Id);

            // Configure the LicenseNo property
            builder.Property(dp => dp.Certification)
                   .IsRequired()           // Mark as required
                   .HasMaxLength(50);      // Set a maximum length

            builder.HasOne(dp => dp.User)
                    .WithOne(u => u.LabTechnician)
                    .HasForeignKey<LabTechnicianProfile>(dp => dp.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
