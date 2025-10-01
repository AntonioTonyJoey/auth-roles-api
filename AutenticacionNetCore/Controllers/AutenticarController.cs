using AutenticacionNetCore.Types;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RepositorioRoles;
using System.DirectoryServices.ActiveDirectory;
using System.Runtime.InteropServices;
namespace AutenticacionNetCore.Controllers
{
    public class AutenticarController : ControllerBase
    {
        private IAD activeDirectoryRepository;
        private IRepositorioRoles rolRepository;

        const int LOGON32_PROVIDER_WINNT50 = 3;
        const int LOGON32_LOGON_NETWORK = 3;
        const string DOMAIN = "TBAD";
        //const string DOMAIN = "Americas";
        IntPtr token;

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool LogonUser(string lpszUsername, string lpszDomain, string lpszPassword,
            int dwLogonType, int dwLogonProvider, out IntPtr phToken);

        public AutenticarController(IAD repoAD, IRepositorioRoles repoR) { 
            activeDirectoryRepository = repoAD;
            rolRepository = repoR;
        }

        [HttpPost]
        [Route("api/[controller]/autenticar")]
        public async Task<JsonResult> Post([FromBody] Input parametros)
        {
            string userName = parametros.user.Trim().ToUpper();
            string password = parametros.pwd.Trim();
            DataAutenticate retorno = new DataAutenticate();
            retorno.active = await activeDirectoryRepository.ADValidation(userName, password);
            retorno.active.isValid = await Task.Run(() =>
            {
                return LogonUser(userName, DOMAIN, password, LOGON32_LOGON_NETWORK, LOGON32_PROVIDER_WINNT50, out token);
            });
            retorno.kronos = new KronosType();
            retorno.Roles = await rolRepository.RolesGet(userName);
            
            return new JsonResult(retorno);
        }
    }
}
