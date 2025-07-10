using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeCareHMS.User;

namespace TakeCareHMS.Identitiy.ModelConfigurations;

public class NurseProfileConfiguration : IEntityTypeConfiguration<NurseProfile>
{
    public void Configure(EntityTypeBuilder<NurseProfile> builder)
    {
        builder.HasKey(dp => dp.Id);

        // Configure the Specialization property
        builder.Property(dp => dp.Specialization)
               .IsRequired(false)           // Mark as required (if desired)
               .HasMaxLength(100);     // Set a maximum length

    }
}
