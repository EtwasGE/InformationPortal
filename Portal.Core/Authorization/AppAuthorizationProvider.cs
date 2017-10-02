using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace Portal.Core.Authorization
{
    public class AppAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            context.CreatePermission(PermissionNames.PagesUsers, L("Users"));
            context.CreatePermission(PermissionNames.PagesRoles, L("Roles"));
            context.CreatePermission(PermissionNames.PagesTenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);

            context.CreatePermission(PermissionNames.ContentChange, L("ContentChange"));
            context.CreatePermission(PermissionNames.ContentAdd, L("ContentAdd"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, CoreConsts.LocalizationSourceName);
        }
    }
}
