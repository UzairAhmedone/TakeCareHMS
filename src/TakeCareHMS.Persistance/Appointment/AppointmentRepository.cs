using TakeCareHms.Repositories;
using TakeCareHMS.Persistance;

namespace TakeCareHms.Appointment;

public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
{
    public AppointmentRepository(TakeCareHmsEntityContext context) : base(context)
    {
    }

    public async Task<DbResults> BookAsync(Appointment appointment, CancellationToken cancellationToken = default)
    {
        return await base.AddAsync(appointment, cancellationToken);
    }
}
