using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeCareHMS.User;

namespace TakeCareHMS.Profiles
{
    internal class PharmacistsProfileConfiguration : IEntityTypeConfiguration<PharmacistProfile>
    {
        public void Configure(EntityTypeBuilder<PharmacistProfile> builder)
        {
            // Set the primary key
            builder.HasKey(dp => dp.Id);

            // Configure the LicenseNo property
            builder.Property(dp => dp.LicenseNo)
                   .IsRequired()           // Mark as required
                   .HasMaxLength(50);      // Set a maximum length

            builder.HasOne(dp => dp.User)
                    .WithOne(u => u.Pharmacist)
                    .HasForeignKey<PharmacistProfile>(dp => dp.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
