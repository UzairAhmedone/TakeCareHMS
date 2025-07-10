using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeCareHMS.User;

namespace TakeCareHMS.Profiles
{
    internal class DoctorProfileConfiguration : IEntityTypeConfiguration<DoctorProfile>
    {
        public void Configure(EntityTypeBuilder<DoctorProfile> builder)
        {
            // Set the primary key
            builder.HasKey(dp => dp.Id);

            // Configure the Specialization property
            builder.Property(dp => dp.Specialization)
                   .IsRequired()           // Mark as required (if desired)
                   .HasMaxLength(100);     // Set a maximum length

            // Configure the LicenseNo property
            builder.Property(dp => dp.LicenseNo)
                   .IsRequired()           // Mark as required
                   .HasMaxLength(50);      // Set a maximum length
            builder.HasOne(dp => dp.User)
                    .WithOne(u => u.Doctor)
                    .HasForeignKey<DoctorProfile>(dp => dp.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
