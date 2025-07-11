using Microsoft.EntityFrameworkCore;

namespace TakeCareHMS.Persistance;

public class TakeCareHmsEntityContext : DbContext
{
    public TakeCareHmsEntityContext(DbContextOptions<TakeCareHmsEntityContext> context) : base(context)
    {       
    }
}
