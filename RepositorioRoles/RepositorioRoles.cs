using AutenticacionNetCore.Datos;
using AutenticacionNetCore.Models;
using AutenticacionNetCore.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositorioRoles
{
    public class RepositorioRoles : IRepositorioRoles
    {
        private readonly ApplicationDbContext _context;

        public RepositorioRoles(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<RolesType>> RolesGet(string WIW)
        {
            IEnumerable<RolesType> roles1 = new List<RolesType>();

            //roles = _context.PROGRAMAs
            //        .Where(x => x.OWNER.Trim() == WIW.Trim())
            //        .Include(p => p.ROLEs)
            //        .Select(p => new RolesType
            //        {
            //            Program = p.PROGRAMA1,
            //            Roles = p.ROLEs.Select(r => r.NOMBRE)
            //        })
            //        .ToList();

            var registros = await _context.REL_ROL_GRUPOs
                .Where(r => r.WIW.Trim() == WIW.Trim() && r.ACTIVO)
                .Select(r => new
                {
                    ProgramaId = r.ID_PROGRAMA,
                    RolId = r.ID_ROL
                })
                .ToListAsync();
            var programaIds = registros.Select(r => r.ProgramaId).Distinct().ToList();
            var programas = await _context.PROGRAMAs
                .Where(p => programaIds.Contains(p.ID))
                .ToDictionaryAsync(p => p.ID, p => p.PROGRAMA1);
            var rolIds = registros.Select(r => r.RolId).Distinct().ToList();
            var roles = await _context.ROLEs
                .Where(r => rolIds.Contains(r.ID))
                .ToDictionaryAsync(r => r.ID, r => r.NOMBRE);

            var resultado = registros
                .GroupBy(r => r.ProgramaId)
                .Select(g =>
                {
                    var nombrePrograma = programas.ContainsKey((Guid)g.Key) ? programas[(Guid)g.Key] : "Sin Programa";

                    var nombresRoles = g
                        .Where(x => x.RolId != null && roles.ContainsKey(x.RolId.Value))
                        .Select(x => roles[x.RolId.Value])
                        .Distinct()
                        .ToList();

                    return new RolesType
                    {
                        Program = nombrePrograma,
                        Roles = nombresRoles
                    };
                })
                .ToList();

            return resultado;

        }
    }
}
