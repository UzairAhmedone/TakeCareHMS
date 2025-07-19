using Microsoft.Extensions.DependencyInjection;
using TakeCareHms.Repositories;

namespace TakeCareHms.Extensions;

public static class PersistanceExtensions
{
    public static IServiceCollection AddPersistanceServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        return services;
    }
}
