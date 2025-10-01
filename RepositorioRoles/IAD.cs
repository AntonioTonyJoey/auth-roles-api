using AutenticacionNetCore.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioRoles
{
    public interface IAD
    {
        Task<ADType> ADValidation(string user, string pwd);
        Task<ADType> DetailsGet(string user);
    }
}
