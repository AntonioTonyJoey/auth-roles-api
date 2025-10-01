using AutenticacionNetCore.Types;
using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


using System.DirectoryServices;



namespace RepositorioRoles
{
    public class AD : IAD
    {
        string ldapDomain = "domain";
        string ldapServer = "server";
        public async Task<ADType> ADValidation(string user, string pwd)
        {
            ADType datos = await DetailsGet(user);
            return datos;
        }
        
        public async Task<ADType> DetailsGet(string user)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (var pc = new PrincipalContext(ContextType.Domain))
                {
                    ADType retornoInterno = new ADType();
                    try
                    {
                        var userinfo = UserPrincipal.FindByIdentity(pc, IdentityType.SamAccountName, @"TBAD\" + user);
                        retornoInterno.DisplayName = userinfo.DisplayName;
                        retornoInterno.FullName = userinfo.GivenName + " " + userinfo.Surname;
                        retornoInterno.Email = userinfo.EmailAddress;
                        retornoInterno.Phone = userinfo.VoiceTelephoneNumber;
                        retornoInterno.WIW = user;
                    }
                    catch (Exception e)
                    {
                        retornoInterno.DisplayName = "";
                        retornoInterno.FullName = "";
                        retornoInterno.Email = "";
                        retornoInterno.Phone = "";
                        retornoInterno.WIW = user;
                    }

                    return retornoInterno;

                }

            });

        }
    }
}
