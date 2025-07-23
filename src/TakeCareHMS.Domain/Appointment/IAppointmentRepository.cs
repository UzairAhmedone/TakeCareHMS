using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TakeCareHms.Repositories;

namespace TakeCareHms.Appointment;

public interface IAppointmentRepository
{
    Task<DbResults> BookAsync(Appointment appointment, CancellationToken cancellationToken = default);
}
