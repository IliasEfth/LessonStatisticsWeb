using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using StatisticsWebModels;
using System.ComponentModel.DataAnnotations.Schema;
using StatisticsWebRepository.IRepository;
using StatisticsWebRepository.Repository;

namespace StatisticsWeb.Authentication
{
    public class UsersAuthentication: OAuthAuthorizationServerProvider
    {
        private static IRepos database = new MySqlDB();
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            ClaimsIdentity id = new ClaimsIdentity(context.Options.AuthenticationType);
            User user = database.userExists(new User() { Name = context.UserName, Password = context.Password });
            if (user != null)
            {
                id.AddClaim(new Claim(ClaimTypes.Role, "student"));
                id.AddClaim(new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()));                
                context.Validated(id);
            }
            else
            {
                context.SetError("credentials", "Invalid credantials was given");
                return;
            }
        }
    }
}