using Abp.Authorization;
using Portal.Core.Authorization.Roles;
using Portal.Core.Authorization.Users;

namespace Portal.Core.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
