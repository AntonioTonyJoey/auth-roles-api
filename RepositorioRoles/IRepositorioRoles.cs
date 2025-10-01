using AutenticacionNetCore.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioRoles
{
    public interface IRepositorioRoles
    {
        Task<IEnumerable<RolesType>> RolesGet(string WIW);
    }
}
