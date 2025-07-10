using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TakeCareHMS.Identitiy;

internal class UserRepository
{
    private readonly TakeCareHmsIdentityContext identityContext;

    internal UserRepository(TakeCareHmsIdentityContext identityContext)
    {
        this.identityContext = identityContext;
    }
}
