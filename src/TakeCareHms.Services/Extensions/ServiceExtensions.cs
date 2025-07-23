using Microsoft.Extensions.DependencyInjection;
using TakeCareHms.Appointment;

namespace TakeCareHms.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddHmsServices(this IServiceCollection services)
    {
        //services.AddPersistanceServices();
        services.AddScoped(typeof(IAppointmentService), typeof(AppointmentService));
        return services;
    }
}
