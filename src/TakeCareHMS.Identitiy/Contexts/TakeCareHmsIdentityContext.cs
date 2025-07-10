using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TakeCareHMS.Identitiy.Configurations;

namespace TakeCareHMS.Identitiy;

internal class TakeCareHmsIdentityContext : IdentityDbContext<IdentityUser>
{
    public TakeCareHmsIdentityContext(DbContextOptions<TakeCareHmsIdentityContext> options)
            : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.RegisterIdentityModelConfigs();
    }
}
