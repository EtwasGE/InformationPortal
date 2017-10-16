using System.DirectoryServices.AccountManagement;
using Abp.Zero.Ldap.Authentication;
using Abp.Zero.Ldap.Configuration;
using Portal.Core.Authorization.Users;
using Portal.Core.MultiTenancy;

namespace Portal.Core.Authorization
{
    public class LdapAuthenticationSource : LdapAuthenticationSource<Tenant, User>
    {
        public LdapAuthenticationSource(ILdapSettings settings, IAbpZeroLdapModuleConfig ldapModuleConfig) 
            : base(settings, ldapModuleConfig)
        {
        }
        
        protected override void UpdateUserFromPrincipal(User user, UserPrincipal userPrincipal)
        {
            user.UserName = userPrincipal.SamAccountName;
            user.Name = userPrincipal.GivenName;
            user.Surname = userPrincipal.Surname;
            user.EmailAddress = userPrincipal.EmailAddress ?? userPrincipal.UserPrincipalName;

            if (string.IsNullOrEmpty(userPrincipal.MiddleName))
            {
                var names = userPrincipal.Name.Split(' ');
                if (names.Length == 3)
                {
                    user.MiddleName = names[2];
                }
            }
            else
            {
                user.MiddleName = userPrincipal.MiddleName;
            }
            //TODO: Not working in IIS. Always return false.
            //if (userPrincipal.Enabled.HasValue)
            //{
            //    user.IsActive = userPrincipal.Enabled.Value;
            //}
        }
    }
}
