using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TakeCareHms.Appointment;
using TakeCareHms.Repositories;
using TakeCareHMS.Persistance;

namespace TakeCareHms.Extensions;

public static class PersistanceExtensions
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TakeCareHmsEntityContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"))); services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IAppointmentRepository), typeof(AppointmentRepository));
        return services;
    }
}
