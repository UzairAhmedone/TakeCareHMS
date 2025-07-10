using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TakeCareHMS.User;

namespace TakeCareHMS.Identitiy.Configurations
{
 
    public class HmsUserConfiguration : IEntityTypeConfiguration<HmsUser>
    {
        public void Configure(EntityTypeBuilder<HmsUser> builder)
        {
            builder.ToTable("HmsUsers"); // Rename Identity table if needed
        }
    }
}
