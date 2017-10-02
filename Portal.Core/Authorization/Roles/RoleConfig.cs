using System.Collections.Generic;
using Abp.MultiTenancy;
using Abp.Zero.Configuration;

namespace Portal.Core.Authorization.Roles
{
    public static class RoleConfig
    {
        public static void Configure(IRoleManagementConfig roleManagementConfig)
        {
            //Static host roles

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Host.Admin,
                    MultiTenancySides.Host)
                );

            //Static tenant roles

            roleManagementConfig.StaticRoles.AddRange(
                new List<StaticRoleDefinition>
                {
                    new StaticRoleDefinition(
                        StaticRoleNames.Tenants.Admin,
                        MultiTenancySides.Tenant),
                    new StaticRoleDefinition(
                        StaticRoleNames.Tenants.Approver,
                        MultiTenancySides.Tenant),
                    new StaticRoleDefinition(
                        StaticRoleNames.Tenants.User,
                        MultiTenancySides.Tenant)
                });
        }
    }
}
